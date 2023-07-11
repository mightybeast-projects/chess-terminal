using Chess.Core;
using Chess.Core.Pieces;
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

        while (GameIsNotOver(game))
            inputHandler.Run();
    }

    private bool GameIsNotOver(Game game) =>
       !game.currentPlayer.king.isChecked &&
       !game.currentPlayer.king.isCheckmated &&
       !game.currentPlayer.king.isInStalemate;
}