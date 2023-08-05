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
            var scaledPosition = Vector3.Transform(position, scaleMatrix);
            Console.WriteLine(
                $"({position}) -> ({scaledPosition})");
        }
    }
    
    /// <summary>
    /// Z軸回りの回転
    /// </summary>
    [Test]
    public void RotateRoll()
    {
        CalculateRotate(new Vector3(50, 40, 0), 90);
        CalculateRotate(new Vector3(100, 40, 0), 90);
        CalculateRotate(new Vector3(75, 200, 0), 90);
        
        void CalculateRotate(Vector3 position, float degree)
        {
            var rotateMatrix = new Matrix4x4((float)Math.Cos(ToRadian(degree)), -(float)Math.Sin(ToRadian(degree)), 0, 0,
                (float)Math.Sin(ToRadian(degree)), (float)Math.Cos(ToRadian(degree)), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
            var positionMatrix = new Matrix4x4(position.X, 0, 0, 0,
                position.Y, 0, 0, 0,
                position.Z, 0, 0, 0,
                0, 0, 0, 0);
            var calculateMatrix = Matrix4x4.Multiply(rotateMatrix, positionMatrix);
            Console.WriteLine(
                $"({positionMatrix.M11},{positionMatrix.M21},{positionMatrix.M31}) -> ({calculateMatrix.M11},{calculateMatrix.M21},{calculateMatrix.M31})");

        }
    }
    /// <summary>
    /// Z軸回りの回転
    /// </summary>
    [Test]
    public void RotationZ()
    {
        CalculateRotate(new Vector3(50, 40, 0), 90);
        CalculateRotate(new Vector3(100, 40, 0), 90);
        CalculateRotate(new Vector3(75, 200, 0), 90);
        
        void CalculateRotate(Vector3 position, float degree)
        {
            var rotateMatrix = Matrix4x4.CreateRotationZ((float)ToRadian(degree));
            var scaledPosition = Vector3.Transform(position, rotateMatrix);
            Console.WriteLine(
                $"({position}) -> ({scaledPosition})");
        }
    }
    /// <summary>
    /// Y軸回りの回転
    /// </summary>
    [Test]
    public void RotateYaw()
    {
        CalculateRotate(new Vector3(100, 0, -50), 180);
        CalculateRotate(new Vector3(400, -40, 0), 180);
        CalculateRotate(new Vector3(-20, 100, 50), 180);
        
        void CalculateRotate(Vector3 position, float degree)
        {
            var rotateMatrix = new Matrix4x4((float)Math.Cos(ToRadian(degree)), 0, (float)Math.Sin(ToRadian(degree)), 0,
                0, 1, 0, 0,
                (float)Math.Sin(ToRadian(degree)), 0, (float)Math.Cos(ToRadian(degree)), 0,
                0, 0, 0, 1);
            var positionMatrix = new Matrix4x4(position.X, 0, 0, 0,
                position.Y, 0, 0, 0,
                position.Z, 0, 0, 0,
                0, 0, 0, 0);
            var calculateMatrix = Matrix4x4.Multiply(rotateMatrix, positionMatrix);
            Console.WriteLine(
                $"({positionMatrix.M11},{positionMatrix.M21},{positionMatrix.M31}) -> ({calculateMatrix.M11},{calculateMatrix.M21},{calculateMatrix.M31})");

        }
    }
    
    /// <summary>
    /// X軸回りの回転
    /// </summary>
    [Test]
    public void RotatePitch()
    {
        CalculateRotate(new Vector3(0, 30, -100), 45);
        CalculateRotate(new Vector3(-50, 100, -20), 45);
        CalculateRotate(new Vector3(-20, 0, -300), 45);
        
        void CalculateRotate(Vector3 position, float degree)
        {
            var rotateMatrix = new Matrix4x4(
                1, 0, 0, 0,
                0, (float)Math.Cos(ToRadian(degree)), -(float)Math.Sin(ToRadian(degree)), 0,
                0, (float)Math.Sin(ToRadian(degree)), (float)Math.Cos(ToRadian(degree)), 0,
                0, 0, 0, 1);
            var positionMatrix = new Matrix4x4(position.X, 0, 0, 0,
                position.Y, 0, 0, 0,
                position.Z, 0, 0, 0,
                0, 0, 0, 0);
            var calculateMatrix = Matrix4x4.Multiply(rotateMatrix, positionMatrix);
            Console.WriteLine(
                $"({positionMatrix.M11},{positionMatrix.M21},{positionMatrix.M31}) -> ({calculateMatrix.M11},{calculateMatrix.M21},{calculateMatrix.M31})");

        }
    }
    
    [Test]
    public void RotatePitchCreatePitch()
    {
        //座標系が違う？
        CalculateRotate(new Vector3(0, 30, -100), 45);
        CalculateRotate(new Vector3(-50, 100, -20), 45);
        CalculateRotate(new Vector3(-20, 0, -300), 45);
        
        void CalculateRotate(Vector3 position, float degree)
        {
            var rotateMatrix = Matrix4x4.CreateRotationX(ToRadian(degree));
            Console.WriteLine(
                $"({position}) -> ({Vector3.Transform(position, rotateMatrix)})");
        }        
    }
    
    /// <summary>
    /// z軸30度roll,x軸180度pitch,y軸90yaw
    /// </summary>
    /// <remarks>OpenGL形式</remarks>
    [Test]
    public void Combine1()
    {
        var roll = Matrix4x4.CreateRotationZ(ToRadian(30));
        var pitch = Matrix4x4.CreateRotationX(ToRadian(180));
        var yaw = Matrix4x4.CreateRotationY(ToRadian(90));
        // 逆順に計算する
        var combine = yaw * pitch * roll;
        var position = new Vector3(200, 0, -30);
        var positionMatrix = new Matrix4x4(position.X, 0, 0, 0,
            position.Y, 0, 0, 0,
            position.Z, 0, 0, 0,
            0, 0, 0, 0);
        var calculateMatrix = Matrix4x4.Multiply(combine, positionMatrix);
        Console.WriteLine(
            $"({positionMatrix.M11},{positionMatrix.M21},{positionMatrix.M31}) -> ({calculateMatrix.M11},{calculateMatrix.M21},{calculateMatrix.M31})");
    }
    /// <summary>
    /// z軸30度roll,x軸180度pitch,y軸90yaw
    /// </summary>
    /// <remarks>DirectX形式</remarks>
    [Test]
    public void Combine2()
    {
        var roll = Matrix4x4.CreateRotationZ(ToRadian(30));
        var pitch = Matrix4x4.CreateRotationX(ToRadian(180));
        var yaw = Matrix4x4.CreateRotationY(ToRadian(90));
        var combine = roll * pitch * yaw;
        var position = new Vector3(200, 0, -30);
        var scaledPosition = Vector3.Transform(position, combine);
        Console.WriteLine(
            $"({position}) -> ({scaledPosition})");

    }
    
    /// <summary>
    /// 中心点周りに-90度回転させるための組み合わせ行列
    /// </summary>
    [Test]
    public void Combine3()
    {
        var center = new Vector3(10, 50, 0);
        var slide = Matrix4x4.CreateTranslation(-center);
        var roll = Matrix4x4.CreateRotationZ(ToRadian(-90));
        var reverseSlide = Matrix4x4.CreateTranslation(center);
        var matrix = slide * roll * reverseSlide;
        var position = new Vector3(-10, 50, 0);
        var transform = Vector3.Transform(position, matrix);
        Console.WriteLine(
            $"({position}) -> ({transform})");
        position = new Vector3(90, 0, 0);
        transform = Vector3.Transform(position, matrix);
        Console.WriteLine(
            $"({position}) -> ({transform})");
        position = new Vector3(-50, 100, 0);
        transform = Vector3.Transform(position, matrix);
        Console.WriteLine(
            $"({position}) -> ({transform})");

    }

    /// <summary>
    /// 3次元の物体を45Roll,90Pitch,-90Yaw
    /// </summary>
    [Test]
    public void Combine4()
    {
        var matrix = Matrix4x4.CreateRotationZ(ToRadian(45)) * Matrix4x4.CreateRotationX(ToRadian(90)) *
                     Matrix4x4.CreateRotationY(ToRadian(-90));
        var position = new Vector3(200, 0, -30);
        var transform = Vector3.Transform(position, matrix);
        Console.WriteLine(
            $"({position}) -> ({transform})");
        position = new Vector3(0, 50, -150);
        transform = Vector3.Transform(position, matrix);
        Console.WriteLine(
            $"({position}) -> ({transform})");
        position = new Vector3(40, 20, -100);
        transform = Vector3.Transform(position, matrix);
        Console.WriteLine(
            $"({position}) -> ({transform})");
    }

    private static float ToRadian(double degree)
    {
        return (float)(degree * Math.PI / 180f);
    }
}