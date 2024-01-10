import kotlin.io.path.Path
import kotlin.io.path.readLines
import kotlin.collections.ArrayList

fun main() {




    val list: List<String> = getFile("input.txt")

    println(part1(list))
    println(part2(list))
}

fun calcB(numList: ArrayList<Long>): Long {
    var ret = ArrayList<ArrayList<Long>>()
    ret.add(numList)
    ret.add(ArrayList<Long>() )
    var pos: Int = 0
    while (true){
        for (n in ret[pos].indices){
            ret[pos+1].add(ret[pos][n+1]-ret[pos][n])
            if(n+1 == ret[pos].size-1){
                break
            }
        }
        if(ret.last().asSequence().all { it == 0L }){ //would mean that this is "mapped"
            break
        }
        pos++
        ret.add(ArrayList<Long>())
    }

    var prevVal: Long = 0
    for (i in ret.indices.reversed()){
        if(i == ret.size-1){ //since this will always be 0 or the last element
            ret[i].add(ret[i].last())
            prevVal = ret[i].last()
            continue
        }
        ret[i].add(ret[i].last()+prevVal)
        prevVal = ret[i].last()
    }

    return ret[0].last()
}

fun part1(input: List<String>): Long {
    var ret = 0L
    for (line in input){
        var numSplit = line.split(" ")
        var numList = ArrayList<Long>()
        for (num in numSplit){
            numList.add(num.toLong())
        }
        ret += calcB(numList)

    }
    return ret
}

fun part2(input: List<String>): Long {
    var ret = 0L
    for (line in input){
        var numSplit = line.split(" ")
        var numList = ArrayList<Long>()
        for (num in numSplit){
            numList.add(num.toLong())
        }
        numList.reverse()
        ret += calcB(numList)

    }
    return ret
}


fun getFile(name: String ) = Path("src/$name").readLines()