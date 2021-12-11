
var game = null
var piecesPath = 'img/chesspieces/{piece}.png'

export function createChessBoardPlay(boardId) {

    game = new Chess()

    var chessBoard = Chessboard(boardId, {
        draggable: true,
        dropOffBoard: 'trash',
        position: 'start',
        pieceTheme: piecesPath,
        onDragStart: onDragStartHandler,
        onDrop: onDropHandler,
        onSnapEnd: function () { chessBoard.position(game.fen()) }
    });

    return chessBoard
}

export function createChessBoardReplay(boardId) {

    game = new Chess();

    var chessBoard = Chessboard(boardId, {
        pieceTheme: piecesPath,
        moveSpeed: 'slow',
        position: 'start'
    });

    return chessBoard
}



function onDragStartHandler(source, piece, position, orientation) {

    if (game.game_over()) {
        return false
    }

    if ((game.turn() === 'w' && piece.search(/^b/) !== -1) ||
        (game.turn() === 'b' && piece.search(/^w/) !== -1)) {
        return false
    }
}

function onDropHandler(source, target) {
    var move = game.move({
        from: source,
        to: target,
        promotion: 'q' // NOTE: always promote to a queen for example simplicity
    })

    if (move === null) {
        return 'snapback'
    }
}

export function makeMove(position) {
    game.move(position);
    return game.fen();
}

export function undoMove() {
    game.undo()
    return game.fen()
}

export function getGameHistory() {  
    return game.history()
}