using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entite = M01_Entite;

namespace M01_DAL_Municipalite_SQLServer;

public class DepotClefAPI:Entite.IDepotClefAPI
{
    private MunicipaliteContextSQLServer m_contexte;


    public DepotClefAPI(MunicipaliteContextSQLServer p_contexte)
    {
        if (p_contexte is null)
        {
            throw new ArgumentNullException(nameof(p_contexte));
        }
        
        this.m_contexte = p_contexte;
    }
    
    public Entite.ClefAPIEntite? ChercherClefAPI()
    {
        return this.m_contexte.ClefApi
            .FirstOrDefault()
            ?.VersEntite();
    }


    public void AjouterClefAPI(Entite.ClefAPIEntite p_clefAPIEntite)
    {
        if (p_clefAPIEntite is null)
        {
            throw new ArgumentNullException(nameof(p_clefAPIEntite));
        }

        if (this.m_contexte.ClefApi.Any(m => m.ClefAPIId == p_clefAPIEntite.CleApIfId))
        {
            ModifierClefAPI(p_clefAPIEntite);
        }
        else
        {
            this.m_contexte.ClefApi.Add(new ClefAPI_DTO(p_clefAPIEntite));
            this.m_contexte.SaveChanges();
        }
    }

    public void ModifierClefAPI(Entite.ClefAPIEntite p_clefAPIEntite)
    {
        if (p_clefAPIEntite is null)
        {
            throw new ArgumentNullException(nameof(p_clefAPIEntite));
        }

        if (!this.m_contexte.ClefApi.Any(m => m.ClefAPIId == p_clefAPIEntite.CleApIfId))
        {
            throw new ArgumentException("Clef API doesn't exist into the depot");
        }
        ClefAPI_DTO clefAPI = new ClefAPI_DTO(p_clefAPIEntite);
        this.m_contexte.ClefApi.Add(clefAPI);
        this.m_contexte.SaveChanges();
    }
  }