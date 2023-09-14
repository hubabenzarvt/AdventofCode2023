namespace AOC2023.Day10;

public class CathodeRayTube
{
    /// <summary>
    /// Create a list of all executions
    /// Create a list of all commands WAITING to be executed
    /// create a list of commands to BEING executed
    /// create a list of commands that HAVE executed
    /// 
    /// StartExecution()
    /// Iterate through the commands
    /// if it starts with noop then don't add it to the execute list
    /// else
    /// Add to execute list
    ///
    /// Executed()
    /// Get the final item from the executed list and modify the X value based on the amount
    /// If the executed amount is 20 then increment of 40 multiply x by the cycle times.
    /// Remove that executed command from the executed list
    /// Add the executed command to the executed list
    ///
    /// Controller()
    /// foreach command in the waiting to execute list
    /// StartExecution()
    /// Executed()
    ///
    ///
    /// </summary>
    public void Part1()
    {
        
    }
}