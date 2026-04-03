using NBomber.CSharp;
using NBomber.Contracts;
using Capa_de_procesamiento_de_datos;

var ingresosLogic = new Ingresos();

var scenario = Scenario.Create("Prueba_Ingresos_Caso15", async context =>
{
    try
    {
        int resultado = ingresosLogic.IngresarIngresos(
            DateTime.Now,
            "Carga Caso 15",
            5000,
            1001,
            2,
            1045,
            "Test_Load"
        );

        return resultado > 0
            ? Response.Ok<object>()
            : Response.Fail<object>("Error: No se generó ID");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ ERROR: {ex.Message}");
        return Response.Fail<object>(ex.Message);
    }
})
.WithoutWarmUp()
.WithLoadSimulations(
    Simulation.IterationsForConstant(copies: 1, iterations: 1000)  // ← exactamente 100 y para
);

NBomberRunner
    .RegisterScenarios(scenario)
    .Run();