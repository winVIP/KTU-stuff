increment(Array) :-
    Count is 0,
    increment_rows(Array, Count).

increment_rows([Row|Rows], Count) :-
    is_list(Row) ->
        increment_row(Row, SubCount),
        NewCount is Count + SubCount,
        increment_rows(Rows, NewCount);
    NewCount is Count + 1,
    increment_rows(Rows, NewCount).
increment_rows([], NewCount) :-
    write('List element count: '),
    write(NewCount), nl.

increment_row(Row, Count) :-
    length(Row, Count).