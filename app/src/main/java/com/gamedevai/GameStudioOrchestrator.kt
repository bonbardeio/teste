package com.gamedevai

class GameStudioOrchestrator {

    private val designer = GameDesignerAgent()
    private val programmer = ProgrammerAgent()
    private val levelDesigner = LevelDesignerAgent()
    private val artist = ArtistAgent()
    private val tester = TesterAgent()
    private val buildAgent = BuildAgent()

    fun generate(command: String, onStep: (Int, String) -> Unit): GameProject {
        onStep(10, "1/9 Gerando conceito...")
        val concept = designer.run(command)

        onStep(20, "2/9 Documento de design...")
        val designDoc = "GDD automático baseado no comando '$command'."

        onStep(35, "3/9 Gerando mapa...")
        val map = levelDesigner.run(command)

        onStep(50, "4/9 Criando personagens...")
        val characters = artist.run(command)

        onStep(65, "5/9 Gerando mecânicas...")
        val gameplay = programmer.run(command)

        onStep(75, "6/9 Criando interface e HUD...")
        val ui = "HUD responsiva, menu inicial e painel de inventário implementados."

        onStep(85, "7/9 Testes automáticos...")
        val report = tester.run(command)

        onStep(95, "8/9 Correção automática...")
        val fixes = "Correções aplicadas em colisão e fluxo de progressão."

        onStep(100, "9/9 Exportando jogo...")
        val build = buildAgent.run(command)

        return GameProject(concept, designDoc, map, characters, "$gameplay\n$fixes", ui, report, build)
    }
}
