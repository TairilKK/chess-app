using System;

namespace chess.Exceptions;

public class NotEmptySquare(string message) : Exception(message)
{

}
