
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace StreetFighterGame
{

using System;
    using System.Collections.Generic;
    
public partial class TAI_KHOAN
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public TAI_KHOAN()
    {

        this.LICH_SU_TRAN_DAU = new HashSet<LICH_SU_TRAN_DAU>();

    }


    public int UserId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public Nullable<int> Tien { get; set; }

    public Nullable<int> SoTranThang { get; set; }

    public Nullable<int> SoTranThua { get; set; }

    public Nullable<int> SoTranHoa { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<LICH_SU_TRAN_DAU> LICH_SU_TRAN_DAU { get; set; }

    public virtual XEP_HANG XEP_HANG { get; set; }

}

}
