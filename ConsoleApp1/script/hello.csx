StringBuilder builder = new StringBuilder();
foreach(var item in Args) {
    builder.Append(item);
}

Console.WriteLine(builder.ToString());