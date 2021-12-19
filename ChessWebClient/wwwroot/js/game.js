var game = null
var chessBoard = null
var dotNetReference = null
var minimaxDepth = 3
var piecesPath = 'images/chesspieces/{piece}.png'

var $board = null
var game = new Chess()
var squareClass = 'square-55d63'
var squareToHighlight = null
var colorToHighlight = null

export function createChessBoardPlay(boardId, dotNetRef) {

    game = new Chess()
    $board = $('#'+boardId)
    dotNetReference = dotNetRef

    chessBoard = Chessboard(boardId, {
        draggable: true,
        dropOffBoard: 'trash',
        position: 'start',
        pieceTheme: piecesPath,
        onDragStart: onDragStartHandler,
        onDrop: onDropHandler,
        onSnapEnd: onSnapEndHandler,
        onMoveEnd: onMoveEndHandler,
        onSnapbackEnd: onSnapbackEndHandler,
        onDragMove: onDragMoveHandler
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

    if (orientation === 'black') {
        $board.find('.square-' + source).addClass('highlight-black')
    }
}

function onSnapEndHandler(source, target, piece) {
    chessBoard.position(game.fen())
}

function onDropHandler(source, target) {
    var move = game.move({
        from: source,
        to: target,
        promotion: 'q' 
    })

    if (move === null) {
        if (game.turn() === 'b') {
            $board.find('.square-' + target).removeClass('highlight-black')
        }
        return 'snapback'
    }

    if (game.turn() === 'b') {
        $board.find('.' + squareClass).removeClass('highlight-white')
        $board.find('.square-' + source).addClass('highlight-white')
        $board.find('.square-' + target).addClass('highlight-white')
    }
    else {
        $board.find('.' + squareClass).removeClass('highlight-black')
        $board.find('.square-' + source).addClass('highlight-black')
        $board.find('.square-' + target).addClass('highlight-black')
	}


    /*var history_arr = game.history()
    console.log(history_arr[history_arr.length - 1])
    console.log('1')*/ //TODO

    window.setTimeout(makeBestMove, 200)
}

function onMoveEndHandler() {
    $board.find('.square-' + squareToHighlight)
        .addClass('highlight-' + colorToHighlight)
}

function onSnapbackEndHandler(piece, square, position, orientation) {
    if (orientation === 'black') {
        $board.find('.square-' + square).removeClass('highlight-black')
    }
}

function onDragMoveHandler(newLocation, oldLocation, source,
    piece, position, orientation) {

    if (orientation === 'black') {
        if (source !== oldLocation) {
            $board.find('.square-' + oldLocation).removeClass('highlight-black')
        }
        $board.find('.square-' + newLocation).addClass('highlight-black')
    }
}

export function makeBestMove() {

	var moveToMake = calculateBestMove()

	console.log(moveToMake)

	if (moveToMake === null) {
        dotNetReference.invokeMethodAsync("GameOver", false)
        return
    }

    if (game.turn() === 'w') {
        $board.find('.' + squareClass).removeClass('highlight-white')
        $board.find('.square-' + moveToMake.from).addClass('highlight-white')
        squareToHighlight = moveToMake.to
        colorToHighlight = 'white'
    } else {
        $board.find('.' + squareClass).removeClass('highlight-black')
        $board.find('.square-' + moveToMake.from).addClass('highlight-black')
        squareToHighlight = moveToMake.to
        colorToHighlight = 'black'
    }

    game.move(moveToMake)
    chessBoard.position(game.fen())

    if (game.game_over()) {
        dotNetReference.invokeMethodAsync("GameOver", true)
    }
}

export function restartGame() {
    chessBoard.position('start')
    game = new Chess()
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

/* Calculating best move */

var calculateBestMove = function () {

	var possibleNextMoves = game.moves({ verbose: true });

	if (possibleNextMoves.length === 0) {
		return null
    }

	var bestMove = -9999;
	var bestMoveFound;

	for (var i = 0; i < possibleNextMoves.length; i++) {
		var possibleNextMove = possibleNextMoves[i]
		game.move(possibleNextMove.san);
		var value = minimax(minimaxDepth, -10000, 10000, false);
		game.undo();
		if (value >= bestMove) {
			bestMove = value;
			bestMoveFound = possibleNextMove;
		}
	}
	return bestMoveFound;
};


var minimax = function (depth, alpha, beta, isMaximisingPlayer) {
	if (depth === 0) {
		return -evaluateBoard(game.board());
	}

	var possibleNextMoves = game.moves();
	var numPossibleMoves = possibleNextMoves.length

	if (isMaximisingPlayer) {
		var bestMove = -9999;
		for (var i = 0; i < numPossibleMoves; i++) {
			game.move(possibleNextMoves[i]);
			bestMove = Math.max(bestMove, minimax(depth - 1, alpha, beta, !isMaximisingPlayer));
			game.undo();
			alpha = Math.max(alpha, bestMove);
			if (beta <= alpha) {
				return bestMove;
			}
		}

	} else {
		var bestMove = 9999;
		for (var i = 0; i < numPossibleMoves; i++) {
			game.move(possibleNextMoves[i]);
			bestMove = Math.min(bestMove, minimax(depth - 1, alpha, beta, !isMaximisingPlayer));
			game.undo();
			beta = Math.min(beta, bestMove);
			if (beta <= alpha) {
				return bestMove;
			}
		}
	}

	return bestMove;
};


var evaluateBoard = function (board) {
	var totalEvaluation = 0;
	for (var i = 0; i < 8; i++) {
		for (var j = 0; j < 8; j++) {
			totalEvaluation = totalEvaluation + getPieceValue(board[i][j], i, j);
		}
	}
	return totalEvaluation;
};


var reverseArray = function (array) {
	return array.slice().reverse();
};

var whitePawnEval =
	[
		[0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0],
		[5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0],
		[1.0, 1.0, 2.0, 3.0, 3.0, 2.0, 1.0, 1.0],
		[0.5, 0.5, 1.0, 2.5, 2.5, 1.0, 0.5, 0.5],
		[0.0, 0.0, 0.0, 2.0, 2.0, 0.0, 0.0, 0.0],
		[0.5, -0.5, -1.0, 0.0, 0.0, -1.0, -0.5, 0.5],
		[0.5, 1.0, 1.0, -2.0, -2.0, 1.0, 1.0, 0.5],
		[0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0]
	];

var blackPawnEval = reverseArray(whitePawnEval);

var knightEval =
	[
		[-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0],
		[-4.0, -2.0, 0.0, 0.0, 0.0, 0.0, -2.0, -4.0],
		[-3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0],
		[-3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0],
		[-3.0, 0.0, 1.5, 2.0, 2.0, 1.5, 0.0, -3.0],
		[-3.0, 0.5, 1.0, 1.5, 1.5, 1.0, 0.5, -3.0],
		[-4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0],
		[-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0]
	];

var whiteBishopEval = [
	[-2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0],
	[-1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0],
	[-1.0, 0.0, 0.5, 1.0, 1.0, 0.5, 0.0, -1.0],
	[-1.0, 0.5, 0.5, 1.0, 1.0, 0.5, 0.5, -1.0],
	[-1.0, 0.0, 1.0, 1.0, 1.0, 1.0, 0.0, -1.0],
	[-1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, -1.0],
	[-1.0, 0.5, 0.0, 0.0, 0.0, 0.0, 0.5, -1.0],
	[-2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0]
];

var blackBishopEval = reverseArray(whiteBishopEval);

var whiteRookEval = [
	[0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0],
	[0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5],
	[-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5],
	[-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5],
	[-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5],
	[-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5],
	[-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5],
	[0.0, 0.0, 0.0, 0.5, 0.5, 0.0, 0.0, 0.0]
];

var blackRookEval = reverseArray(whiteRookEval);

var evalQueen = [
	[-2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0],
	[-1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0],
	[-1.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0],
	[-0.5, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5],
	[0.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5],
	[-1.0, 0.5, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0],
	[-1.0, 0.0, 0.5, 0.0, 0.0, 0.0, 0.0, -1.0],
	[-2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0]
];

var whiteKingEval = [

	[-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0],
	[-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0],
	[-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0],
	[-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0],
	[-2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0],
	[-1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0],
	[2.0, 2.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0],
	[2.0, 3.0, 1.0, 0.0, 0.0, 1.0, 3.0, 2.0]
];

var blackKingEval = reverseArray(whiteKingEval);


var getPieceValue = function (piece, x, y) {
	if (piece === null) {
		return 0;
	}

	var absoluteValue = getAbsoluteValue(piece, piece.color === 'w', x, y);

	if (piece.color === 'w') {
		return absoluteValue;
	} else {
		return -absoluteValue;
	}
};


var getAbsoluteValue = function (piece, isWhite, x, y) {
	if (piece.type === 'p') {
		return 10 + (isWhite ? whitePawnEval[y][x] : blackPawnEval[y][x]);
	} else if (piece.type === 'r') {
		return 50 + (isWhite ? whiteRookEval[y][x] : blackRookEval[y][x]);
	} else if (piece.type === 'n') {
		return 30 + knightEval[y][x];
	} else if (piece.type === 'b') {
		return 30 + (isWhite ? whiteBishopEval[y][x] : blackBishopEval[y][x]);
	} else if (piece.type === 'q') {
		return 90 + evalQueen[y][x];
	} else if (piece.type === 'k') {
		return 900 + (isWhite ? whiteKingEval[y][x] : blackKingEval[y][x]);
	}
};