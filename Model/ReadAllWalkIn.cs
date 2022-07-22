using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace walk_in_api.Model
{
    public class ReadAllWalkInResponse
    {
        public bool IsSuccess {get; set;}
        public string? Message{get; set;}

        public List<GetReadAllWalkIn>? readAllWalkIns {get; set;}
        
    }

    public class GetReadAllWalkIn 
    {



        // public DateTime startDate{get; set;} 
        //  public DateTime endDate{get; set;} 
        // public DateTime expiresBy{get; set;} 
        // public String? jobRoleTitle{get; set;}
        // public String? walkInTitle{get; set;} 

        // public TimeOnly timeStart{get; set;}
         
        // public TimeOnly timeEnd{get; set;}
        // public String? city {get; set;}

         public String? startDate{get; set;} 
         public String? endDate{get; set;} 
        public String? expiresBy{get; set;} 
        public String? jobRoleTitle{get; set;}
        public String? walkInTitle{get; set;} 

        public String? timeStart{get; set;}
         
        public String? timeEnd{get; set;}
        public String? city {get; set;}
 
    }
}