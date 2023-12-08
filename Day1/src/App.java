import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;

import java.util.List;
import java.util.Map;
import java.util.Scanner;
import java.util.stream.Collectors;

public class App {

    public static final Map<String,String> DICT_MAP = Map.of(
                                "one","1", 
                                "two","2",
                                "three","3",
                                "four","4",
                                "five","5",
                                "six","6",
                                "seven","7",
                                "eight","8",
                                "nine","9");                           


    public static void main(String[] args) throws Exception {
        int sumPart1 = 0;
        int sumPart2 = 0;
        var listOfInputs = List.copyOf(fileInput("input.txt"));
        var intListPart1 = listOfInputs.stream()
                 .map(App::findInts)
                 .collect(Collectors.toList()); 
        
        var intListPart2 = listOfInputs.stream()
                        .map(App::transformWordsToNumber)
                        .map(App::findInts)
                        .collect(Collectors.toList());

        for (Integer num : intListPart1) {
            sumPart1 += num;
        }

        for (Integer num : intListPart2) {
            sumPart2 += num;
        }

        System.out.println("part one sum: " + sumPart1);
        System.out.println("----");
        System.out.println("part two sum: " + sumPart2);
    }







    private static String transformWordsToNumber(String word){
        String wordFront = word;
        String wordBackwards = word;
        var listOfNumbers = List.copyOf(DICT_MAP.keySet());
        if (wordFront.length() > 4){
            for(int i = 0, j = 4; j<wordFront.length();i++,j++){
                var tmp = wordFront.substring(i, j+1);
                for (String words : listOfNumbers) {
                    if(tmp.indexOf(words) != -1){
                        wordFront = wordFront.replace(words, DICT_MAP.get(words));
                    }
                }
            }
                for(int j = wordBackwards.length()-1, i = j-4; i>=0;i--,j--){
                var tmp = wordBackwards.substring(i, j+1);
                for (String words : listOfNumbers) {
                    if(tmp.indexOf(words) != -1){
                        wordBackwards = wordBackwards.replace(words, DICT_MAP.get(words));
                        j = wordBackwards.length()-1;
                        i = j-4;
                        break;
                    }
                }
            }

            
        }
        return (wordFront+wordBackwards);
    }

    private static int findInts(String word){
        int foundInt = 0;
        char firstInt = ' ';
        char secondInt = ' ';
        for (int i = 0; i < word.length(); i++) {
            if(Character.isDigit(word.charAt(i))){
                firstInt = word.charAt(i);
                break;
            }
        }
        for (int i = word.length()-1; i >= 0; i--) {
            if(Character.isDigit(word.charAt(i))){
                secondInt = word.charAt(i);
                break;
            }
        }
        if(firstInt == ' ' || secondInt == ' '){
            throw new NullPointerException();
        }
        StringBuilder sb = new StringBuilder();
        sb.append(firstInt);
        sb.append(secondInt);
        foundInt = Integer.parseInt(sb.toString());
        return foundInt;
    }

    private static final List<String> fileInput(String name){
        List<String> list = new ArrayList<String>();
        
        try {
            File rFile = new File(name);
            Scanner rScanner = new Scanner(rFile);
            while (rScanner.hasNextLine()) {
                String data = rScanner.nextLine();
                list.add(data);
            }
        rScanner.close();
        } catch (FileNotFoundException e) {
            System.out.println("An error occurred.");
            e.printStackTrace();
        }
        return list;
    }
}
