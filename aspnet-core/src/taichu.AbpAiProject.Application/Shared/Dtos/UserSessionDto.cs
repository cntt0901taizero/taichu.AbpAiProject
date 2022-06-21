using System;
using System.Collections.Generic;

namespace BaseApplication.Dtos
{
    public class UserSessionDto
    {
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int? CoSoId { get; set; }
        public string TenCoSo { get; set; }
        public int? LoaiCoSo { get; set; }
        public int? Cap { get; set; }
        public int? KhuVucId { get; set; }
        public int? TinhId { get; set; }
        public int? HuyenId { get; set; }
        public int? XaId { get; set; }
        public List<string> GrantedPermissions { get; set; }
        public bool IsSuperAdmin { get; set; } = false;
    }
}
