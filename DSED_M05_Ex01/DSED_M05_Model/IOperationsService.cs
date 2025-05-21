using System.Numerics;
using System.ServiceModel;

namespace DSED_M05_Model;

[ServiceContract]
public interface IOperationsService
{
    [OperationContract]
    float Additionner(float p1,  float p2);
                                   
    [OperationContract]
    float Soustraire(float p1, float p2);
    
    [OperationContract]
    float Multiplier(float p1, float p2);
    
    [OperationContract]
    float Diviser(float p1, float p2);
    
    [OperationContract]
    float RacineCarrer(float p1);
    
    [OperationContract]
    string Echo(string echo);
}