#import system.

#class SourceData
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