import org.antlr.v4.runtime.ParserRuleContext;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.misc.Pair;
import org.antlr.v4.runtime.misc.Triple;
import org.antlr.v4.runtime.tree.ErrorNode;
import org.antlr.v4.runtime.tree.ParseTree;
import org.antlr.v4.runtime.tree.TerminalNode;

import javax.sound.sampled.Line;
import java.util.*;

public class MyListener extends gBaseListener{
    private Map<String, Integer> intvars;
    private Map<String, Float> floatvars;
    private Map<String, String> stringvars;
    private Map<String, Boolean> boolvars;

    public MyListener(){
        intvars = new HashMap<>();
        floatvars = new HashMap<>();
        stringvars = new HashMap<>();
        boolvars = new HashMap<>();
    }

    @Override
    public void exitIfas(gParser.IfasContext ctx) {
        if(ctx.condition() != null){
            exitCondition(ctx.condition());
            if(Conditionresult != true){
                return;
            }
        }
        if(ctx.intervalC() != null){
            exitIntervalC(ctx.intervalC());
            if(IntervalCresult != true){
                return;
            }
        }
        List<gParser.LineContext> lines = ctx.line();
    }

    @Override public void exitAssign(gParser.AssignContext ctx) {
        //jeigu ctx.invokingState == 76, tai kvieciamas is declare parser rulo
        //System.out.println("Isejom i a");
        if(ctx.invokingState == 76){
            return;
        }

        String var = ctx.WORD(0).getText();

        //System.out.println(ctx.operation() != null);
        if(ctx.operation() != null){
            exitOperation(ctx.operation());
            if(OperationResultIsInt){
                intvars.put(var, OperationResultInt);
            }
            else if(OperationResultIsFloat){
                floatvars.put(var, OperationResultFloat);
            }
            //System.out.println("isInt: " + OperationResultIsInt + " Value: " + OperationResultInt);
            //System.out.println("isFloat: " + OperationResultIsInt + " Value: " + OperationResultInt);
            return;
        }
        if(ctx.intervalC() != null){
            exitIntervalC(ctx.intervalC());
            boolvars.put(var, IntervalCresult);
            return;
        }
        if(ctx.condition() != null){
            exitCondition(ctx.condition());
            boolvars.put(var, Conditionresult);
            return;
        }

        //Checking FLOAT
        if(floatvars.containsKey(var)){
            if(ctx.FLOAT() == null){
                throw new RuntimeException("Types dont match");
            }
            else {
                floatvars.put(var, Float.parseFloat(ctx.FLOAT().getText()));
            }
        }

        //Checking INT
        else if(intvars.containsKey(var)){
            if(ctx.INT() == null){
                throw new RuntimeException("Types dont match");
            }
            else{
                intvars.put(var, Integer.parseInt(ctx.INT().getText()));
            }
        }

        //Checking BOOL
        else if(boolvars.containsKey(var)){
            if(ctx.BOOL() == null){
                throw new RuntimeException("Types dont match");
            }
            else{
                boolvars.put(var, ctx.BOOL().getText().equals("true"));
            }
        }
    }

    @Override
    public void enterPrintToC(gParser.PrintToCContext ctx) {

    }

    @Override public void exitPrintToC(gParser.PrintToCContext ctx) {

        String key = ctx.WORD().getText();
        if(intvars.containsKey(key)){
            System.out.println(intvars.get(key));
        }
        else if(floatvars.containsKey(key)){
            System.out.println(floatvars.get(key));
        }
        else if(stringvars.containsKey(key)){
            System.out.println(stringvars.get(key));
        }
        else if(boolvars.containsKey(key)){
            System.out.println(boolvars.get(key));
        }
        else{
            throw new RuntimeException("Variable has not declared initiated or is null");
        }
    }

    static boolean IntervalCresult;
    @Override
    public void exitIntervalC(gParser.IntervalCContext ctx) {
        boolean value = false;
        List<TerminalNode> ints = ctx.INT();
        List<TerminalNode> floats = ctx.FLOAT();
        List<TerminalNode> words = ctx.WORD();
        List<TerminalNode> lops = ctx.LOPS();
        if(VariablesType(words).equals("not") && words.size() != 0){
            throw new RuntimeException("Variables not same type");
        }
        if(VariablesType(words).equals("bool") && words.size() != 0){
            throw new RuntimeException("Cant compare bool variables");
        }
        if(VariablesType(words).equals("string") && words.size() != 0){
            throw new RuntimeException("Cant compare string variables");
        }
        if(VariablesType(words).equals("int") && words.size() != 0){
            List<Integer> comparers = new ArrayList<>();
            List<Pair<Integer, Integer>> coms = new ArrayList<>();

            for (TerminalNode word: words) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), intvars.get(word.getText())));
            }
            for (TerminalNode word: ints) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), Integer.valueOf(word.getText())));
            }
            coms.sort(new Comparator<Pair<Integer, Integer>>() {
                @Override
                public int compare(Pair<Integer, Integer> o1, Pair<Integer, Integer> o2) {
                    return o1.a.compareTo(o2.a);
                }
            });
            for (Pair c:coms) {
                System.out.println(c.toString());
                comparers.add(((Integer) c.b));
            }
            if(lops.get(0).getText().equals("==") && lops.get(0).getText().equals("!=")){
                throw new RuntimeException("Cant use this logical operator");
            }
            boolean result1 = false;
            if(lops.get(0).getText().equals("<")){
                result1 = comparers.get(0) < comparers.get(1);
            }
            else if(lops.get(0).getText().equals(">")){
                result1 = comparers.get(0) > comparers.get(1);
            }
            else if(lops.get(0).getText().equals("<=")){
                result1 = comparers.get(0) <= comparers.get(1);
            }
            else if(lops.get(0).getText().equals(">=")){
                result1 = comparers.get(0) >= comparers.get(1);
            }
            boolean result2 = false;
            if(lops.get(1).getText().equals("<")){
                result2 = comparers.get(1) < comparers.get(2);
            }
            else if(lops.get(1).getText().equals(">")){
                result2 = comparers.get(1) > comparers.get(2);
            }
            else if(lops.get(1).getText().equals("<=")){
                result2 = comparers.get(1) <= comparers.get(2);
            }
            else if(lops.get(1).getText().equals(">=")){
                result2 = comparers.get(1) >= comparers.get(2);
            }
            boolean result = result1 && result2;
            IntervalCresult = result;
            //System.out.println(result);
        }
        if(ints.size() == 3 && words.size() == 0){
            List<Integer> comparers = new ArrayList<>();
            for(TerminalNode i: ints){
                    comparers.add(Integer.valueOf(i.getText()));
            }
            if(lops.get(0).getText().equals("==") && lops.get(0).getText().equals("!=")){
                throw new RuntimeException("Cant use this logical operator");
            }
            boolean result1 = false;
            if(lops.get(0).getText().equals("<")){
                result1 = comparers.get(0) < comparers.get(1);
            }
            else if(lops.get(0).getText().equals(">")){
                result1 = comparers.get(0) > comparers.get(1);
            }
            else if(lops.get(0).getText().equals("<=")){
                result1 = comparers.get(0) <= comparers.get(1);
            }
            else if(lops.get(0).getText().equals(">=")){
                result1 = comparers.get(0) >= comparers.get(1);
            }
            boolean result2 = false;
            if(lops.get(1).getText().equals("<")){
                result2 = comparers.get(1) < comparers.get(2);
            }
            else if(lops.get(1).getText().equals(">")){
                result2 = comparers.get(1) > comparers.get(2);
            }
            else if(lops.get(1).getText().equals("<=")){
                result2 = comparers.get(1) <= comparers.get(2);
            }
            else if(lops.get(1).getText().equals(">=")){
                result2 = comparers.get(1) >= comparers.get(2);
            }
            boolean result = result1 && result2;
            IntervalCresult = result;
            //System.out.println(result);
        }

        if(VariablesType(words).equals("float") && words.size() != 0){
            List<Float> comparers = new ArrayList<>();
            List<Pair<Integer, Float>> coms = new ArrayList<>();

            for (TerminalNode word: words) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), floatvars.get(word.getText())));
            }
            for (TerminalNode word: floats) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), Float.valueOf(word.getText())));
            }
            coms.sort(new Comparator<Pair<Integer, Float>>() {
                @Override
                public int compare(Pair<Integer, Float> o1, Pair<Integer, Float> o2) {
                    return o1.a.compareTo(o2.a);
                }
            });
            for (Pair c:coms) {
                comparers.add(((Float) c.b));
            }
            if(lops.get(0).getText().equals("==") && lops.get(0).getText().equals("!=")){
                throw new RuntimeException("Cant use this logical operator");
            }
            boolean result1 = false;
            if(lops.get(0).getText().equals("<")){
                result1 = comparers.get(0) < comparers.get(1);
            }
            else if(lops.get(0).getText().equals(">")){
                result1 = comparers.get(0) > comparers.get(1);
            }
            else if(lops.get(0).getText().equals("<=")){
                result1 = comparers.get(0) <= comparers.get(1);
            }
            else if(lops.get(0).getText().equals(">=")){
                result1 = comparers.get(0) >= comparers.get(1);
            }
            boolean result2 = false;
            if(lops.get(1).getText().equals("<")){
                result2 = comparers.get(1) < comparers.get(2);
            }
            else if(lops.get(1).getText().equals(">")){
                result2 = comparers.get(1) > comparers.get(2);
            }
            else if(lops.get(1).getText().equals("<=")){
                result2 = comparers.get(1) <= comparers.get(2);
            }
            else if(lops.get(1).getText().equals(">=")){
                result2 = comparers.get(1) >= comparers.get(2);
            }
            boolean result = result1 && result2;
            IntervalCresult = result;
            //.out.println(result);
        }
        if(floats.size() == 3 && words.size() == 0){
            List<Float> comparers = new ArrayList<>();
            for(TerminalNode i: floats){
                comparers.add(Float.valueOf(i.getText()));
            }
            if(lops.get(0).getText().equals("==") && lops.get(0).getText().equals("!=")){
                throw new RuntimeException("Cant use this logical operator");
            }
            boolean result1 = false;
            if(lops.get(0).getText().equals("<")){
                result1 = comparers.get(0) < comparers.get(1);
            }
            else if(lops.get(0).getText().equals(">")){
                result1 = comparers.get(0) > comparers.get(1);
            }
            else if(lops.get(0).getText().equals("<=")){
                result1 = comparers.get(0) <= comparers.get(1);
            }
            else if(lops.get(0).getText().equals(">=")){
                result1 = comparers.get(0) >= comparers.get(1);
            }
            boolean result2 = false;
            if(lops.get(1).getText().equals("<")){
                result2 = comparers.get(1) < comparers.get(2);
            }
            else if(lops.get(1).getText().equals(">")){
                result2 = comparers.get(1) > comparers.get(2);
            }
            else if(lops.get(1).getText().equals("<=")){
                result2 = comparers.get(1) <= comparers.get(2);
            }
            else if(lops.get(1).getText().equals(">=")){
                result2 = comparers.get(1) >= comparers.get(2);
            }
            boolean result = result1 && result2;
            IntervalCresult = result;
            //System.out.println(result);
        }
    }

    @Override
    public void enterAssign(gParser.AssignContext ctx) {
        //System.out.println("Iejom i a");
    }

    static boolean Conditionresult;
    @Override
    public void exitCondition(gParser.ConditionContext ctx) {
        //System.out.println(ctx.invokingState);
        List<TerminalNode> ints = ctx.INT();
        List<TerminalNode> floats = ctx.FLOAT();
        List<TerminalNode> words = ctx.WORD();
        TerminalNode lops = ctx.LOPS();

        if(VariablesType(words).equals("not") && words.size() != 0){
            throw new RuntimeException("Variables not same type");
        }
        if(VariablesType(words).equals("bool") && words.size() != 0){
            throw new RuntimeException("Cant compare bool variables");
        }
        if(VariablesType(words).equals("string") && words.size() != 0){
            throw new RuntimeException("Cant compare string variables");
        }
        if(VariablesType(words).equals("int") && words.size() != 0){
            List<Integer> comparers = new ArrayList<>();
            List<Pair<Integer, Integer>> coms = new ArrayList<>();

            for (TerminalNode word: words) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), intvars.get(word.getText())));
            }
            for (TerminalNode word: ints) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), Integer.valueOf(word.getText())));
            }
            coms.sort(new Comparator<Pair<Integer, Integer>>() {
                @Override
                public int compare(Pair<Integer, Integer> o1, Pair<Integer, Integer> o2) {
                    return o1.a.compareTo(o2.a);
                }
            });
            for (Pair c:coms) {
                //System.out.println(c.toString());
                comparers.add(((Integer) c.b));
            }
            boolean result = false;
            if(lops.getText().equals("==")){
                result = comparers.get(0) == comparers.get(1);
            }
            if(lops.getText().equals("<")){
                result = comparers.get(0) < comparers.get(1);
            }
            if(lops.getText().equals(">")){
                result = comparers.get(0) > comparers.get(1);
            }
            if(lops.getText().equals("<=")){
                result = comparers.get(0) <= comparers.get(1);
            }
            if(lops.getText().equals(">=")){
                result = comparers.get(0) >= comparers.get(1);
            }
            if(lops.getText().equals("!=")){
                result = comparers.get(0) != comparers.get(1);
            }
            Conditionresult = result;
            //System.out.println(result);
        }
        if(ints.size() == 2 && words.size() == 0){
            List<Integer> comparers = new ArrayList<>();
            for(TerminalNode i: ints){
                comparers.add(Integer.valueOf(i.getText()));
            }
            boolean result = false;
            if(lops.getText().equals("==")){
                result = comparers.get(0) == comparers.get(1);
            }
            if(lops.getText().equals("<")){
                result = comparers.get(0) < comparers.get(1);
            }
            if(lops.getText().equals(">")){
                result = comparers.get(0) > comparers.get(1);
            }
            if(lops.getText().equals("<=")){
                result = comparers.get(0) <= comparers.get(1);
            }
            if(lops.getText().equals(">=")){
                result = comparers.get(0) >= comparers.get(1);
            }
            if(lops.getText().equals("!=")){
                result = comparers.get(0) != comparers.get(1);
            }
            Conditionresult = result;
            //System.out.println(result);
        }

        if(VariablesType(words).equals("float") && words.size() != 0){
            List<Float> comparers = new ArrayList<>();
            List<Pair<Integer, Float>> coms = new ArrayList<>();

            for (TerminalNode word: words) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), floatvars.get(word.getText())));
            }
            for (TerminalNode word: floats) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), Float.valueOf(word.getText())));
            }
            coms.sort(new Comparator<Pair<Integer, Float>>() {
                @Override
                public int compare(Pair<Integer, Float> o1, Pair<Integer, Float> o2) {
                    return o1.a.compareTo(o2.a);
                }
            });
            for (Pair c:coms) {
                comparers.add(((Float) c.b));
            }
            boolean result = false;
            if(lops.getText().equals("==")){
                result = comparers.get(0) == comparers.get(1);
            }
            if(lops.getText().equals("<")){
                result = comparers.get(0) < comparers.get(1);
            }
            if(lops.getText().equals(">")){
                result = comparers.get(0) > comparers.get(1);
            }
            if(lops.getText().equals("<=")){
                result = comparers.get(0) <= comparers.get(1);
            }
            if(lops.getText().equals(">=")){
                result = comparers.get(0) >= comparers.get(1);
            }
            if(lops.getText().equals("!=")){
                result = comparers.get(0) != comparers.get(1);
            }
            Conditionresult = result;
            //System.out.println(result);
        }
        if(floats.size() == 2 && words.size() == 0){
            List<Float> comparers = new ArrayList<>();
            for(TerminalNode i: floats){
                comparers.add(Float.valueOf(i.getText()));
            }
            boolean result = false;
            if(lops.getText().equals("==")){
                result = comparers.get(0) == comparers.get(1);
            }
            if(lops.getText().equals("<")){
                result = comparers.get(0) < comparers.get(1);
            }
            if(lops.getText().equals(">")){
                result = comparers.get(0) > comparers.get(1);
            }
            if(lops.getText().equals("<=")){
                result = comparers.get(0) <= comparers.get(1);
            }
            if(lops.getText().equals(">=")){
                result = comparers.get(0) >= comparers.get(1);
            }
            if(lops.getText().equals("!=")){
                result = comparers.get(0) != comparers.get(1);
            }
            Conditionresult = result;
            //System.out.println(result);
        }
    }

    static boolean OperationResultIsInt = false;
    static boolean OperationResultIsFloat = false;
    static int OperationResultInt;
    static Float OperationResultFloat;
    @Override
    public void exitOperation(gParser.OperationContext ctx) {
        //System.out.println("Was in operation");
        List<TerminalNode> ints = ctx.INT();
        List<TerminalNode> floats = ctx.FLOAT();
        List<TerminalNode> words = ctx.WORD();
        TerminalNode op = ctx.OPERATORS();
        if(VariablesType(words).equals("not") && words.size() != 0){
            throw new RuntimeException("Variables not same type");
        }
        if(VariablesType(words).equals("bool") && words.size() != 0){
            throw new RuntimeException("Cant do operation on bool variables");
        }
        if(VariablesType(words).equals("string") && words.size() != 0){
            throw new RuntimeException("Cant do operation on string variables");
        }
        if(VariablesType(words).equals("int") && words.size() != 0){
            List<Integer> comparers = new ArrayList<>();
            List<Pair<Integer, Integer>> coms = new ArrayList<>();

            for (TerminalNode word: words) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), intvars.get(word.getText())));
            }
            for (TerminalNode word: ints) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), Integer.valueOf(word.getText())));
            }
            coms.sort(new Comparator<Pair<Integer, Integer>>() {
                @Override
                public int compare(Pair<Integer, Integer> o1, Pair<Integer, Integer> o2) {
                    return o1.a.compareTo(o2.a);
                }
            });
            for (Pair c:coms) {
                //System.out.println(c.toString());
                comparers.add(((Integer) c.b));
            }
            int result = 0;
            if(op.getText().equals("+")){
                result = comparers.get(0) + comparers.get(1);
            }
            else if(op.getText().equals("*")){
                result = comparers.get(0) * comparers.get(1);
            }
            else if(op.getText().equals("-")){
                result = comparers.get(0) - comparers.get(1);
            }
            else if(op.getText().equals("/")){
                result = comparers.get(0) / comparers.get(1);
            }
            OperationResultInt = result;
            OperationResultIsInt = true;
        }
        if(ints.size() == 2 && words.size() == 0){
            List<Integer> comparers = new ArrayList<>();
            for(TerminalNode i: ints){
                comparers.add(Integer.valueOf(i.getText()));
            }
            int result = 0;
            if(op.getText().equals("+")){
                result = comparers.get(0) + comparers.get(1);
            }
            else if(op.getText().equals("*")){
                result = comparers.get(0) * comparers.get(1);
            }
            else if(op.getText().equals("-")){
                result = comparers.get(0) - comparers.get(1);
            }
            else if(op.getText().equals("/")){
                result = comparers.get(0) / comparers.get(1);
            }
            //System.out.println(result);
            OperationResultInt = result;
            OperationResultIsInt = true;
        }

        if(VariablesType(words).equals("float") && words.size() != 0){
            List<Float> comparers = new ArrayList<>();
            List<Pair<Integer, Float>> coms = new ArrayList<>();

            for (TerminalNode word: words) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), floatvars.get(word.getText())));
            }
            for (TerminalNode word: floats) {
                coms.add(new Pair<>(word.getSymbol().getTokenIndex(), Float.valueOf(word.getText())));
            }
            coms.sort(new Comparator<Pair<Integer, Float>>() {
                @Override
                public int compare(Pair<Integer, Float> o1, Pair<Integer, Float> o2) {
                    return o1.a.compareTo(o2.a);
                }
            });
            for (Pair c:coms) {
                comparers.add(((Float) c.b));
            }
            Float result = 0f;
            if(op.getText().equals("+")){
                result = comparers.get(0) + comparers.get(1);
            }
            else if(op.getText().equals("*")){
                result = comparers.get(0) * comparers.get(1);
            }
            else if(op.getText().equals("-")){
                result = comparers.get(0) - comparers.get(1);
            }
            else if(op.getText().equals("/")){
                result = comparers.get(0) / comparers.get(1);
            }
            //System.out.println("Operation result: " +  result);
            OperationResultFloat = result;
            OperationResultIsFloat = true;
        }
        if(floats.size() == 2 && words.size() == 0){
            List<Float> comparers = new ArrayList<>();
            for(TerminalNode i: floats){
                comparers.add(Float.valueOf(i.getText()));
            }
            Float result = 0f;
            if(op.getText().equals("+")){
                result = comparers.get(0) + comparers.get(1);
            }
            else if(op.getText().equals("*")){
                result = comparers.get(0) * comparers.get(1);
            }
            else if(op.getText().equals("-")){
                result = comparers.get(0) - comparers.get(1);
            }
            else if(op.getText().equals("/")){
                result = comparers.get(0) / comparers.get(1);
            }
            //System.out.println("Operation result: " +  result);
            OperationResultFloat = result;
            OperationResultIsFloat = true;
        }
    }



    public boolean isString(TerminalNode word){
        if(stringvars.containsKey(word.getText()) && word.getText() != null){
            System.out.println("Gotten text = " +  word.getText());
            return true;
        }
        else {
            return false;
        }
    }
    public boolean isInt(TerminalNode word){
        if(intvars.containsKey(word.getText())  && word.getText() != null){
            //System.out.println("Was true");
            return true;
        }
        else {
            //System.out.println("Was false");
            return false;
        }
    }
    public boolean isFloat(TerminalNode word){
        if(floatvars.containsKey(word.getText())  && word.getText() != null){
            return true;
        }
        else {
            return false;
        }
    }
    public boolean isBool(TerminalNode word){
        if(boolvars.containsKey(word.getText())  && word.getText() != null){
            return true;
        }
        else {
            return false;
        }
    }
    public String VariablesType(List<TerminalNode> words){
        if(words.size() == 0){
            return "empty";
        }
        boolean typeResult = true;
        for (TerminalNode word: words) {
            typeResult = typeResult && isString(word);
        }
        if(typeResult == true){
            return "string";
        }
        typeResult = true;
        for (TerminalNode word: words) {
            typeResult = typeResult && isInt(word);
        }
        if(typeResult == true){
            return "int";
        }
        typeResult = true;
        for (TerminalNode word: words) {
            typeResult = typeResult && isFloat(word);
        }
        if(typeResult == true){
            return "float";
        }
        typeResult = true;
        for (TerminalNode word: words) {
            typeResult = typeResult && isBool(word);
        }
        if(typeResult == true){
            return "bool";
        }
        return "not";
    }


}
