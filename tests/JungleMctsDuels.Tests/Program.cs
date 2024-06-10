using JungleMctsDuels.Tests;

await Task.WhenAll(
            Task.Run(MctsUctVsMctsUct.Run),
            Task.Run(MctsUctVsMctsBeam.Run),
            Task.Run(MctsUctVsAlphaBeta.Run),
            Task.Run(AlphaBetaVsAlphaBeta.Run),
            Task.Run(MctsBeamVsAlphaBeta.Run),
            Task.Run(MctsBeamVsMctsBeam.Run)
            Task.Run(AlphaBetaVsReflexiveMcts.Run)
            Task.Run(MctsBeamVsReflexiveMcts.Run),
            Task.Run(MctsUctVsReflexiveMcts.Run),
            Task.Run(ReflexiveMctsVsReflexiveMcts.Run)
        );
