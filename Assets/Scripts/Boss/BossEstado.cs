public abstract class BossEstado
{
    protected readonly BossMagoFSM boss;

    protected BossEstado(BossMagoFSM boss)
    {
        this.boss = boss;
    }

    public virtual void Entrar() { }
    public virtual void Atualizar() { }
    public virtual void Sair() { }
}
