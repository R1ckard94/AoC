import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.stream.Collectors;

public class App {
    public static void main(String[] args) throws Exception {
        var listOfInputs = List.copyOf(fileInput("input.txt"));
        List<Integer> timeList = extractNumbers(listOfInputs.get(0));
        List<Integer> distanceList = extractNumbers(listOfInputs.get(1));
        
        StringBuilder sbTime = new StringBuilder();
        StringBuilder sbDist = new StringBuilder();
        for (Integer integer : timeList) {
            sbTime.append(integer);
        }
        for (Integer integer : distanceList) {
            sbDist.append(integer);
        }
        List<Long> timeList2 = List.of(Long.parseLong(sbTime.toString()));
        List<Long> distanceList2 = List.of(Long.parseLong(sbDist.toString()));
        int nPart1 = getMultiplyedValueOfAmountOfWaysToWin(timeList, distanceList);
        Long nPart2 = getMultiplyedValueOfAmountOfWaysToWinPart2(timeList2, distanceList2);

        System.out.println("PART1 -> N = " +  nPart1);
        System.out.println("PART2 -> N = " +  nPart2);

    }

    private static int getMultiplyedValueOfAmountOfWaysToWin(List<Integer> timeList, List<Integer> distanceList){
        int n = 1;
        int margin = 0;

        for (int i = 0; i< timeList.size(); i++) {
            margin = 0;
            for (int hold = 0; hold<timeList.get(i); hold++){
                if((hold * (timeList.get(i)-hold)) > distanceList.get(i)){
                    margin += 1;
                }
            }
            n*=margin;
        }
        return n;
    }
    private static Long getMultiplyedValueOfAmountOfWaysToWinPart2(List<Long> timeList, List<Long> distanceList){
        Long n = 1l;
        Long margin = 0l;

        for (int i = 0; i< timeList.size(); i++) {
            margin = 0l;
            for (long hold = 0l; hold<timeList.get(i); hold++){
                if((hold * (timeList.get(i)-hold)) > distanceList.get(i)){
                    margin += 1;
                }
            }
            n*=margin;
        }
        return n;
    }
    private static List<Integer> extractNumbers(String input) {
        List<Integer> numbers = new ArrayList<>();

        // Define a pattern for extracting integers
        Pattern pattern = Pattern.compile("\\b\\d+\\b");
        Matcher matcher = pattern.matcher(input);

        // Find all matches and add them to the list
        while (matcher.find()) {
            numbers.add(Integer.parseInt(matcher.group()));
        }

        return numbers;
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
