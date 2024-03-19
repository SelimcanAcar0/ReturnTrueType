public class FileSignature
{
    public string Signature { get; set; }
    public List<SignatureDetails> Details { get; set; }
}

public class SignatureDetails
{
    public string Extension { get; set; }
    public string Description { get; set; }
}