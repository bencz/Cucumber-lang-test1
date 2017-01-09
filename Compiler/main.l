#import system.
#import system'dynamic.
#import system'routines.
#import system'collections.
#import system'io.
#import extensions.
#import extensions'routines.
#import extensions'text.

#class Data
{
    #field thePosition.
    #field theString.
    #field theComment.
    #field theSourceInfo.
        
    #constructor new &position:aPosition &SourceInfo:aSourceInfo
    [
        theSourceInfo := aSourceInfo.
        thePosition := aPosition.
    ]
    
    #method getPosition = thePosition.
    #method setPosition:aPosition [ thePosition := aPosition. ]
    
    #method getString = theString.
    #method setString:aString [ theString := aString. ]
    
    #method getComment = theComment.
    #method setComment:aComment [ theComment := aComment. ]
    
    #method getSourceInfo = theSourceInfo.
    #method setSourceInfo:aSourceInfo [ theSourceInfo := aSourceInfo. ]
}

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

#class Tokenizer
{    
    #field theSouceFileName.
    #field theSourceContent.
    
    #constructor new : aFileSource
    [
        theSouceFileName := aFileSource.
        theSourceContent := File new &path:aFileSource wideContent.
    ]
}

#symbol program =
[
    #var tk := Tokenizer new:"..\Tests\tk-txt1.txt".
].