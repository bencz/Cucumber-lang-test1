#import system.

#class SourceInfo
{
    #field theSource.
    #field theLineNumber.
    #field theLinePosition.
    
    #constructor new &sourceFile:aSourceFile &int:aLineNumber &int:aLinePosition
    [
    ]
    
    #method literal
    [
        ^(theSource + ": " + theLineNumber + " " + theLinePosition).
    ]
    
    #method getSourceName = theSource.
    #method getLineNumber = theLineNumber.
    #method getLinePosition = theLinePosition.
}