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

    public void DrawGame(Exception error = null)
    {
        Console.Clear();

        boardDrawer.DrawBoard();

        if (error is not null)
            decorator.DrawError(error);

        DrawGameInfo();
    }

    public void EnableHintsFor(Piece piece) => boardDrawer.hintPiece = piece;

    private void DrawGameInfo()
    {
        if (game.board.LastMovedPieceIsAPawnAvailableForPromotion())
            decorator.DrawPawnPromotionSelector();
        else
            DrawGameStatus();
    }

    private void DrawGameStatus()
    {
        King currentPlayerKing = game.currentPlayer.king;

        if (currentPlayerKing.isCheckmated)
            decorator.DrawCurrentKingIsCheckmated();
        else if (currentPlayerKing.isInStalemate)
            decorator.DrawGameIsInStaleMate();
        else if (currentPlayerKing.isChecked)
            decorator.DrawCurrentKingIsChecked();

        if (!game.isOver)
            decorator.DrawCurrentPlayerInfo();
    }
}