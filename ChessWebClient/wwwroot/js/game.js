var game = null
var chessBoard = null
var dotNetReference = null
var piecesPath = 'images/chesspieces/{piece}.png'

export function createChessBoardPlay(boardId, dotNetRef) {

    game = new Chess()
    dotNetReference = dotNetRef

    chessBoard = Chessboard(boardId, {
        draggable: true,
        dropOffBoard: 'trash',
        position: 'start',
        pieceTheme: piecesPath,
        onDragStart: onDragStartHandler,
        onDrop: onDropHandler,
        onSnapEnd: onSnapEndHandler
    });

    return chessBoard
}

export function createChessBoardReplay(boardId) {

    game = new Chess();

    chessBoard = Chessboard(boardId, {
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

    if ((game.turn() === 'b' && orientation === 'white') ||
        (game.turn() === 'w' && orientation === 'black')) {
        return false
    }

    /*if ((game.turn() === 'w' && piece.search(/^b/) !== -1) ||
        (game.turn() === 'b' && piece.search(/^w/) !== -1)) {
        return false
    }*/
}

function onSnapEndHandler(source, target, piece) {
    chessBoard.position(game.fen())
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

    var history_arr = game.history()
    console.log(history_arr[history_arr.length - 1])
    console.log('1')
    window.setTimeout(makeRandomMove, 250)
}

export function makeMove(position) {
    game.move(position)
    return game.fen()
}

export function undoMove() {
    game.undo()
    return game.fen()
}

export function getGameHistory() {  
    return game.history()
}


export function makeRandomMove() {
    var possibleMoves = game.moves()

    if (possibleMoves.length === 0) {
        dotNetReference.invokeMethodAsync("GameOver", false)
        return
    } 

    var randomIdx = Math.floor(Math.random() * possibleMoves.length)
    game.move(possibleMoves[randomIdx])
    chessBoard.position(game.fen())

    if (game.game_over()) {
        dotNetReference.invokeMethodAsync("GameOver", true)
    }
}

export function restartGame() {
    chessBoard.position('start')
    game = new Chess()
}