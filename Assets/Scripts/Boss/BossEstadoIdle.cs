public class BossEstadoIdle : BossEstado
{
    public BossEstadoIdle(BossMagoFSM boss) : base(boss) { }

    public override void Entrar()
    {
        boss.DefinirVulneravel(false);
        boss.FicarInvisivel(false);
        boss.TocarAnimacao("Idle");
    }
}
