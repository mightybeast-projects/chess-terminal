using Chess.Core;
using Chess.Core.Pieces;

namespace ChessTerminal.Drawer;

public class BoardDrawer
{
    public Piece hintPiece;

    private IPieceDrawerVisitor pieceDrawerVisitor;
    private DrawerDecorator decorator;
    private Board board;
    private Tile currentTile;
    private ConsoleColor bgColor;
    private int tileColorIndex;

    public BoardDrawer(Board board, DrawerDecorator decorator)
    {
        this.board = board;
        this.decorator = decorator;

        pieceDrawerVisitor = new PieceDrawerVisitor();
    }

    public void DrawBoard()
    {
        Console.Clear();

        for (int i = board.grid.GetLength(0) - 1; i >= 0; i--)
            DrawBoardRow(i);

        DisableHints();
    }

    private void DrawBoardRow(int i)
    {
        if (i == 7)
            decorator.DrawLetterLine();

        for (int j = 0; j < board.grid.GetLength(1); j++)
            HandleGridPosition(i, j);

        Console.WriteLine();

        if (i == 0)
            decorator.DrawLetterLine();
    }

    private void HandleGridPosition(int i, int j)
    {
        if (j == 0)
            decorator.DrawNumber(i);

        DrawTile(i, j);

        if (j == 7)
            decorator.DrawNumber(i);
    }

    private void DrawTile(int i, int j)
    {
        currentTile = board.grid[i, j];

        ChooseBackgroundColor();

        if (currentTile.isEmpty)
            Console.Write("  ");
        else
            currentTile.piece.Accept(pieceDrawerVisitor);
    }

    private void ChooseBackgroundColor()
    {
        tileColorIndex = (int)currentTile.color;

        if (CurrentTileIsAHintPieceTile())
            bgColor = ConsoleColor.DarkYellow;
        else if (CurrentTileIsAHint())
            bgColor = ConsoleColor.DarkGreen;
        else
            ChooseSimpleTileColor();

        Console.BackgroundColor = bgColor;
    }

    private void ChooseSimpleTileColor()
    {
        if (tileColorIndex == 0)
            bgColor = ConsoleColor.Black;
        else
            bgColor = ConsoleColor.White;
    }

    private void DisableHints() => hintPiece = null;

    private bool CurrentTileIsAHint() =>
        hintPiece != null &&
        hintPiece.legalMoves.Contains(currentTile);

    private bool CurrentTileIsAHintPieceTile() =>
        hintPiece != null &&
        hintPiece.tile == currentTile;
}