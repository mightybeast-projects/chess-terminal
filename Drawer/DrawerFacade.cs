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

        King currentPlayerKing = game.currentPlayer.king;

        if (currentPlayerKing.isCheckmated)
            decorator.DrawCurrentKingIsCheckmated();
        else if (currentPlayerKing.isInStalemate)
            decorator.DrawGameIsInStaleMate();
        else if (currentPlayerKing.isChecked)
        {
            decorator.DrawCurrentKingIsChecked();
            decorator.DrawCurrentPlayerInfo();
        }
        else
            decorator.DrawCurrentPlayerInfo();
    }

    public void DrawError(Exception e)
    {
        boardDrawer.DrawBoard();

        decorator.DrawError(e);

        decorator.DrawCurrentPlayerInfo();
    }

    public void EnableHintsFor(Piece piece) => boardDrawer.hintPiece = piece;
}