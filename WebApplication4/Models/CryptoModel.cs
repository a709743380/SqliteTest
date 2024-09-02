using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class CryptoModel
    {
        public string? DecodeContent { get; set; }
        public string? EncodeContent { get; set; }
        [Required(ErrorMessage = "Key is required.")]
        public string Key { get; set; }
        [Required(ErrorMessage = "IV is required.")]
        public string Iv { get; set; }
        public e_OPTION Option { get; set; }

        public string Title { get; set; }

    }
    public enum e_OPTION
    {
        Encryp, Decryp, PwdHash, ToBase64
    }
    public class KeyIv
    {
        public string Key { get; set; }
        public string Iv { get; set; }
    }
}
