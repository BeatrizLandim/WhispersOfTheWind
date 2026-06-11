using UnityEngine;

public class BossEstadoRecuperacao : BossEstado
{
    private float timer;

    public BossEstadoRecuperacao(BossMagoFSM boss, float duracao) : base(boss)
    {
        timer = duracao;
    }

    public override void Entrar()
    {
        boss.DefinirVulneravel(true);
        boss.TocarAnimacao("Recuperando");
    }

    public override void Atualizar()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
            boss.TrocarEstado(new BossEstadoIdle(boss));
    }

    public override void Sair()
    {
        boss.DefinirVulneravel(false);
    }
}
