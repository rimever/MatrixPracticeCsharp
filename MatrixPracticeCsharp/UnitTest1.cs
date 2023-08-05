using System.Numerics;

namespace MatrixPracticeCsharp;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// 平行移動
    /// </summary>
    [Test]
    public void Slide()
    {
        var slide = new Matrix4x4(1, 0, 0, 200,
            0, 1, 0, 20,
            0, 0, 1, 0,
            0, 0, 0, 1);
        var l = new Matrix4x4(-100, 0, 0, 0,
            30, 0, 0, 0,
            0, 0, 0, 0,
            1, 0, 0, 0);
        var lSlide = Matrix4x4.Multiply(slide, l);
        Console.WriteLine($"({l.M11},{l.M21}) -> ({lSlide.M11},{lSlide.M21})");
        var m = new Matrix4x4(50, 0, 0, 0,
            -80, 0, 0, 0,
            0, 0, 0, 0,
            1, 0, 0, 0);
        var mSlide = Matrix4x4.Multiply(slide, m);
        Console.WriteLine($"({m.M11},{m.M21}) -> ({mSlide.M11},{mSlide.M21})");
        var n = new Matrix4x4(70, 0, 0, 0,
            -0, 0, 0, 0,
            0, 0, 0, 0,
            1, 0, 0, 0);
        var nSlide = Matrix4x4.Multiply(slide, n);
        Console.WriteLine($"({n.M11},{n.M21}) -> ({nSlide.M11},{nSlide.M21})");
    }

    [Test]
    public void Scale2d()
    {
        var scale = new Matrix4x4(3, 0, 0, 0,
            0, 3, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 1);
        CalculateScale(10, 10);
        CalculateScale(50, 10);
        CalculateScale(50, 40);
        CalculateScale(10, 40);

        void CalculateScale(float x, float y)
        {
            var position = new Matrix4x4(x, 0, 0, 0,
                y, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0);
            var scaledPosition = Matrix4x4.Multiply(scale, position);
            Console.WriteLine($"({position.M11},{position.M21}) -> ({scaledPosition.M11},{scaledPosition.M21})");
        }
    }

    [Test]
    public void UnstableScale2d()
    {
        var scale = new Matrix4x4(1.5f, 0, 0, 0,
            0, 0.1f, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 1);
        CalculateScale(20, 0);
        CalculateScale(50, 0);
        CalculateScale(50, 100);
        CalculateScale(20, 100);

        void CalculateScale(float x, float y)
        {
            var position = new Matrix4x4(x, 0, 0, 0,
                y, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0);
            var scaledPosition = Matrix4x4.Multiply(scale, position);
            Console.WriteLine($"({position.M11},{position.M21}) -> ({scaledPosition.M11},{scaledPosition.M21})");
        }
    }

    [Test]
    public void Scale3d()
    {
        var scale = new Matrix4x4(1f, 0, 0, 0,
            0, 2f, 0, 0,
            0, 0, 0.5f, 0,
            0, 0, 0, 1);
        CalculateScale(50, 0, 10);
        CalculateScale(0, 20, -100);
        CalculateScale(200, 150, -50);

        void CalculateScale(float x, float y, float z)
        {
            var position = new Matrix4x4(x, 0, 0, 0,
                y, 0, 0, 0,
                z, 0, 0, 0,
                0, 0, 0, 0);
            var scaledPosition = Matrix4x4.Multiply(scale, position);
            Console.WriteLine(
                $"({position.M11},{position.M21},{position.M31}) -> ({scaledPosition.M11},{scaledPosition.M21},{scaledPosition.M31})");
        }
    }

    [Test]
    public void Scale3dCreateScale()
    {
        CalculateScale(new Vector3(-50, 20, 0), new Vector3(0.5f, 0.5f, 0.5f));
        CalculateScale(new Vector3(-10, 20, 0), new Vector3(0.5f, 0.5f, 0.5f));
        CalculateScale(new Vector3(-30, 40, 0), new Vector3(0.5f, 0.5f, 0.5f));

        CalculateScale(new Vector3(-50, 20, 0), new Vector3(0.25f, 3f, 0.5f));
        CalculateScale(new Vector3(-10, 20, 0), new Vector3(0.25f, 3f, 0.5f));
        CalculateScale(new Vector3(-30, 40, 0), new Vector3(0.25f, 3f, 0.5f));

        CalculateScale(new Vector3(0, 30, -100), new Vector3(10f, 10f, 10f));
        CalculateScale(new Vector3(-50, 100, -20), new Vector3(10f, 10f, 10f));
        CalculateScale(new Vector3(-20, 0, -300), new Vector3(10f, 10f, 10f));

        CalculateScale(new Vector3(0, 30, -100), new Vector3(2f, 1f, 0.5f));
        CalculateScale(new Vector3(-50, 100, -20), new Vector3(2f, 1f, 0.5f));
        CalculateScale(new Vector3(-20, 0, -300), new Vector3(2f, 1f, 0.5f));


        void CalculateScale(Vector3 position, Vector3 scale)
        {
            var scaleMatrix = Matrix4x4.CreateScale(scale);
            var positionMatrix = new Matrix4x4(position.X, 0, 0, 0,
                position.Y, 0, 0, 0,
                position.Z, 0, 0, 0,
                0, 0, 0, 0);
            var scaledPosition = Matrix4x4.Multiply(scaleMatrix, positionMatrix);
            Console.WriteLine(
                $"({positionMatrix.M11},{positionMatrix.M21},{positionMatrix.M31}) -> ({scaledPosition.M11},{scaledPosition.M21},{scaledPosition.M31})");
        }
    }
}