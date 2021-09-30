start(Original, New) :-
    multiply(Original, [], New).

multiply([Head|Tail], Empty, New) :-
    getTwo(Head, First, Second),
    (
        number(Second) ->
        multiplyNumber(First, Second, Second, Result),
        append(Empty, [[Result]], TempList),
        multiply(Tail, TempList, New);

        multiplyString(First, Second, Second, Result),
        append(Empty, [[Result]], TempList),
        multiply(Tail, TempList, New)
    ).
multiply([], Empty, New) :-
    New = Empty.

getTwo([Mult, Element|Tail], First, Second) :-
    First = Mult,
    Second = Element.

multiplyNumber(1, Element, Melement, Result) :-
    Result = Melement.
multiplyNumber(Times, Element, Melement, Result) :-
    NewMelement is Melement * 10 + Element,
    NewTimes is Times - 1,
    multiplyNumber(NewTimes, Element, NewMelement, Result).

multiplyString(1, Element, Melement, Result) :-
    Result = Melement.
multiplyString(Times, Element, Melement, Result) :-
    string_concat(Element, Melement, NewMelement),
    NewTimes is Times - 1,
    multiplyNumber(NewTimes, Element, NewMelement, Result).