using Chess.Core;
using ChessTerminal.Drawer;

namespace ChessTerminal;

public class Terminal
{
    public void Run()
    {
        Game game = new Game();
        DrawerFacade drawer = new DrawerFacade(game);
        InputHandler inputHandler = new InputHandler(game, drawer);

        game.Start();
        drawer.DrawGame();

        while (true)
            inputHandler.Run();
    }
}