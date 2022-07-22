using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace walk_in_api.Model
{
    public class ReadWalkInByIdRequest
    {
        [Required(ErrorMessage = "walk in id is required")]
        public int WalkInID{get; set;}   
    }

    public class GetReadWalkInById 
    {



        

         public String? startDate{get; set;} 
         public String? endDate{get; set;} 
        public String? expiresBy{get; set;} 
        public String? jobRoleTitle{get; set;}
        public String? walkInTitle{get; set;} 

        public String? timeStart{get; set;}
         
        public String? timeEnd{get; set;}
        public String? city {get; set;}

        public Object? description {get;set;}
        public Object? requirements {get;set;}
        public int compensation {get;set;}
 
    }

    public class ReadWalkInByIdResponse
    {
        public bool IsSuccess {get; set;}
        public string? Message {get; set;}
        public List<GetReadWalkInById>? readWalkInByIds {get; set;}
        
        
    }
}