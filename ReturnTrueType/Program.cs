using Newtonsoft.Json;


// JSON verilerini oku
string jsonData = File.ReadAllText("config\\signature.json"); //! change from the current path to the full path
Dictionary<string, List<SignatureDetails>> signatureDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<SignatureDetails>>>(jsonData);



string filePath = "FİLE_PATH";
// Dosyanın imzasını al


if (!File.Exists(filePath))
{
    Console.WriteLine("File Not FOund");
    return;
}
string fileSignature = GetFileSignature(filePath);
string currentFileExtension = Path.GetExtension(filePath);
Console.WriteLine($"Signature :{fileSignature}");
Console.WriteLine($"Current File Extension : {currentFileExtension}");


// Dosyanın imzası null değilse karşılaştır
if (fileSignature != null)
{
    // JSON verilerinde imza ara
    foreach (var item in signatureDictionary)
    {
        if (item.Key.Replace(" ", string.Empty).StartsWith(fileSignature))
        {
            Console.WriteLine("\n True Type File ");
            foreach (var detail in item.Value)
            {
                Console.WriteLine($"Uzantı: {detail.Extension}, Açıklama: {detail.Description}");
            }
            return;
        }
    }

    Console.WriteLine("No matching signature found.");
}
else
{
    Console.WriteLine("File not found or an error occurred while reading.");
}


// Dosyanın imzasını almak için yardımcı yöntem
string GetFileSignature(string filePath)
{
    try
    {
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            byte[] buffer = new byte[4]; // İmza için yeterli olacak kadar bir tampon
            int bytesRead = stream.Read(buffer, 0, 4);
            if (bytesRead == 4)
            {
                return BitConverter.ToString(buffer).Replace("-", "");
            }
            else
            {
                return null;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Catch Exception: {ex.Message}");
        return null;
    }
}
