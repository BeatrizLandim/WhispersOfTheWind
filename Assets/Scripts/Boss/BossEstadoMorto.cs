public class BossEstadoMorto : BossEstado
{
    public BossEstadoMorto(BossMagoFSM boss) : base(boss) { }

    public override void Entrar()
    {
        boss.DefinirVulneravel(false);
        boss.FicarInvisivel(false);
        boss.TocarAnimacao("Morto");
        boss.enabled = false;
    }
}
