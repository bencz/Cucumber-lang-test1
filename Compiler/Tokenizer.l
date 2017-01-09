#import system.
#import system'dynamic.
#import system'routines.
#import system'collections.
#import system'io.
#import extensions.
#import extensions'routines.
#import extensions'text.

#class Tokenizer
{    
    #field theSouceFileName.
    #field theSourceContent.
    
    //------------
    #field theLinePositions.
    #field theTokens.
    #field theResults.
    
    #field theSourcePosition.
    #field theLineNumber.
    #field theLinePosition.
    
    #constructor new : aFileSource
    [
        theSouceFileName := aFileSource.
        theSourceContent := File new &path:aFileSource wideContent.
        
        theLinePositions := Stack new.
        theTokens := Stack new.
        theResults := Stack new.
        
        theSourcePosition := 0.
        theLineNumber := 1.
        theLinePosition := 1.
    ]
    
    #method read
    [
    ]
    
    #method skipSpaces
    [
    ]
    
    #method rewind
    [
    ]
    
    #method peek
    [
    ]
    
    #method readInternal
    [
    ]
    
    #method canRead
    [
        ^((theSourceContent != $nil) && (theSourcePosition < (theSourceContent length))).
    ]
    
    #method getSourceInfo
    [
        (theResults length > 0)
            ? [ ^theResults pop getSourceInfo. ].
        self skipSpaces.
        ^(SourceInfo new &sourceFile:theSouceFileName &int:theLineNumber &int:theLinePosition).
    ]
}