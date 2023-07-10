using Chess.Core;
using Chess.Core.Pieces;

namespace ChessTerminal.Drawer;

public class DrawerFacade
{
    private DrawerDecorator decorator;
    private BoardDrawer boardDrawer;
    private Game game;

    public DrawerFacade(Game game)
    {
        this.game = game;

        decorator = new DrawerDecorator(game);
        boardDrawer = new BoardDrawer(game.board, decorator);

        if (OperatingSystem.IsWindows())
            Console.OutputEncoding = System.Text.Encoding.Unicode;
    }

    public void DrawGame()
    {
        boardDrawer.DrawBoard();

        decorator.DrawCurrentPlayerInfo();
    }

    public void DrawError(Exception e)
    {
        boardDrawer.DrawBoard();

        decorator.DrawError(e.Message);

        decorator.DrawCurrentPlayerInfo();
    }

    public void EnableHintsFor(Piece piece) => boardDrawer.hintPiece = piece;
}