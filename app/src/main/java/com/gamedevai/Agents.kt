package com.gamedevai

interface StudioAgent {
    val name: String
    fun run(input: String): String
}

class GameDesignerAgent : StudioAgent {
    override val name = "Game Designer Agent"
    override fun run(input: String): String =
        "Conceito criado para '$input': loop principal, progressão e economia base definidas."
}

class ProgrammerAgent : StudioAgent {
    override val name = "Programmer Agent"
    override fun run(input: String): String =
        "Scripts gerados: player controller, combate, inventário e sistema de missões."
}

class LevelDesignerAgent : StudioAgent {
    override val name = "Level Designer Agent"
    override fun run(input: String): String =
        "Mapa procedural criado com zonas iniciais, intermediárias e arena final."
}

class ArtistAgent : StudioAgent {
    override val name = "Artist Agent"
    override fun run(input: String): String =
        "Sprites e texturas low-poly gerados para tema '$input'."
}

class TesterAgent : StudioAgent {
    override val name = "Tester Agent"
    override fun run(input: String): String =
        "Testes automáticos executados: 17 casos, 2 bugs corrigidos automaticamente."
}

class BuildAgent : StudioAgent {
    override val name = "Build Agent"
    override fun run(input: String): String =
        "Build concluída: /storage/emulated/0/GameDevAI/exports/${input.replace(" ", "_")}.apk"
}
