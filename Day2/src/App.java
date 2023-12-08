import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.stream.Collectors;

public class App {
    public static int RED_CUBES = 0;
    public static int GREEN_CUBES = 0;
    public static int BLUE_CUBES = 0;


    public static void main(String[] args) throws Exception {
        int sumPart1 = 0;
        var listOfInputs = List.copyOf(fileInput("input.txt"));
        var listOfGoodGames = listOfInputs.stream()
                                .map(App::FilterGames)
                                .collect(Collectors.toList());
        //listOfGoodGames.forEach(System.out::println);
        for (Integer num : listOfGoodGames) {
            sumPart1 += num;
            if(num == 0){
                throw new NullPointerException("number is invalid");
            }
            System.out.println(sumPart1);
        }
       // System.out.println(sumPart1);
        
    }


    
    public static int FilterGames(String word){
        int gameNumber = 0;
        Pattern gamePattern = Pattern.compile("Game (\\d+):");
        Matcher gameMatcher = gamePattern.matcher(word);
        if (gameMatcher.find()) {
            gameNumber = Integer.parseInt(gameMatcher.group(1));
        }
        String[] games = word.split(";");

        // Process each game
        for (String subGame : games) {
            if(!parseGameString(subGame.trim())){
                //System.out.println("Game " + gameNumber + " breaks rules");
                gameNumber = 0;
                break;
            }
        }
        gameNumber = (BLUE_CUBES*RED_CUBES*GREEN_CUBES);
        
        //reset
        RED_CUBES = 0;
        BLUE_CUBES = 0;
        GREEN_CUBES = 0;

        return gameNumber;
    }

    private static boolean parseGameString(String subGame) {
        

        Pattern colorPattern = Pattern.compile("(\\d+)\\s+(\\w+)");

         // Extract color counts
         Matcher colorMatcher = colorPattern.matcher(subGame);
         int redCount = 0;
         int blueCount = 0;
         int greenCount = 0;
 
         while (colorMatcher.find()) {
             int count = Integer.parseInt(colorMatcher.group(1));
             String color = colorMatcher.group(2).toLowerCase();
 
             switch (color) {
                 case "red":
                     redCount += count;
                     break;
                 case "blue":
                     blueCount += count;
                     break;
                 case "green":
                     greenCount += count;
                     break;
                 // Add more cases for additional colors if needed
             }
         }
         if(redCount > RED_CUBES){
            RED_CUBES = redCount;
         }
         if(blueCount > BLUE_CUBES ){
            BLUE_CUBES = blueCount;
         }
         if(greenCount > GREEN_CUBES){
            GREEN_CUBES = greenCount;
         }
        return true;
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
