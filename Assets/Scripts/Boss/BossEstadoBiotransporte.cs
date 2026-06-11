using UnityEngine;

public class BossEstadoBiotransporte : BossEstado
{
    private float timer;
    private Vector2 posicaoColetada;
    private bool sumiu;
    private bool apareceu;

    public BossEstadoBiotransporte(BossMagoFSM boss) : base(boss) { }

    public override void Entrar()
    {
        timer = 0f;
        sumiu = false;
        apareceu = false;

        if (boss.Jogador != null)
            posicaoColetada = boss.Jogador.position;

        boss.DefinirVulneravel(false);
        boss.TocarAnimacao("Biotransporte");
    }

    public override void Atualizar()
    {
        timer += Time.deltaTime;

        if (!sumiu && timer >= boss.BioTempoParaSumir)
        {
            sumiu = true;
            boss.FicarInvisivel(true);
        }

        if (!apareceu && timer >= boss.BioTempoParaSumir + boss.BioTempoSumido)
        {
            apareceu = true;
            AparecerEExplodir();
        }
    }

    private void AparecerEExplodir()
    {
        if (boss.Jogador != null)
        {
            float lado = boss.Jogador.position.x >= posicaoColetada.x ? -1f : 1f;
            Vector2 novaPosicao = posicaoColetada + Vector2.right * lado * boss.BioDistanciaAoLado;
            boss.transform.position = novaPosicao;
        }

        boss.FicarInvisivel(false);
        boss.TocarAnimacao("BioExplosao");
        boss.CausarDanoEmArea(boss.transform.position, boss.BioRaioExplosao, boss.BioDano);
        boss.TrocarEstado(new BossEstadoRecuperacao(boss, boss.BioRecuperacao));
    }
}
