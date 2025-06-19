using System;
using System.ComponentModel.DataAnnotations;
using M01_Entite;

namespace M01_DAL_Municipalite_SQLServer;

public class ClefAPI_DTO
{
    [Key]
    public Guid ClefAPIId { get; set; }

    public ClefAPI_DTO()
    {
        ;
    }
    public ClefAPI_DTO(ClefAPIEntite p_clefAPIEntite)
    {
        this.ClefAPIId = p_clefAPIEntite.CleApIfId;
    }
   
    public ClefAPIEntite VersEntite()
    {
        return new ClefAPIEntite(this.ClefAPIId);
    }
    
}