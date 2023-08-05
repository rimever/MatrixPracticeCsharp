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
}