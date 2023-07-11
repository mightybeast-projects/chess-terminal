using Chess.Core;
using Chess.Core.Pieces;
using ChessTerminal.Drawer;

namespace ChessTerminal;

public class InputHandler
{
    private DrawerFacade drawer;
    private Game game;
    private string input;

    public InputHandler(Game game, DrawerFacade drawer)
    {
        this.game = game;
        this.drawer = drawer;
    }

    public void Run()
    {
        Exception error = null;

        try { HandleInput(); }
        catch (Exception e) { error = e; }

        drawer.DrawGame(error);
    }

    private void HandleInput()
    {
        input = Console.ReadLine();

        if (input is null || input.Length == 0)
            return;

        if (game.board.LastMovedPieceIsAPawnAvailableForPromotion())
            HandleChosenPromotionPiece();
        else
            HandleChosenPlayerPiece();
    }

    private void HandleChosenPromotionPiece()
    {
        if (input.Length != 1)
            return;

        Type pieceType = input switch
        {
            "q" => pieceType = typeof(Queen),
            "b" => pieceType = typeof(Bishop),
            "k" => pieceType = typeof(Knight),
            "r" => pieceType = typeof(Rook),
            _ => typeof(Queen)
        };

        game.PromoteMovedPawnTo(pieceType);
    }

    private void HandleChosenPlayerPiece()
    {
        Piece chosenPiece = game.board.GetTile(input).piece;

        if (InputIsHintCommand())
            drawer.EnableHintsFor(chosenPiece);
        else if (InputIsMoveCommand())
            MoveChosenPiece();
    }

    private void MoveChosenPiece()
    {
        string pieceTileStr = input.Split(" ")[0];
        string targetTileStr = input.Split(" ")[1];
        game.HandlePlayerMove(pieceTileStr, targetTileStr);
    }

    private bool InputIsHintCommand() => input.Length == 2;

    private bool InputIsMoveCommand() => input.Length == 5;
}