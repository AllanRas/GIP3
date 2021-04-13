$(async() => {   
    const resp = await fetch('/PlanningsModules/Event');
    const event = await resp.json();

    calender(event);
   
         
})
function calender(events) {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        events: events,
        //editable: false,
        dayCellContent: function (e) {            
            if (e.date.toISOString().slice(0, 10) == "2021-04-10")
            {
                e.dayNumberText = { html: '<i class= "fas fa-exclamation" >3s</i>'}
            }
                  
        },
       
    });

     var todayDate = new Date().toISOString().slice(0, 10);
     var dvs = $('#index> div');
     for (var i in dvs) {
         const id = dvs[i].id;
         if (id == todayDate) {
             $('#' + id).css('display', 'block');
         }
     }
     calendar.on('dateClick', function (info) {         
         var ob = $('#' + info.dateStr);       
         const parag = $('#Create');
         var dvs = $('#index> div');        
         for (var i in dvs) {
             const id = dvs[i].id;            
             if (id == info.dateStr){
                 parag.css('display', 'none');
                 $('#' + id).css('display', 'block');
                 break;
             }
             else
             {          
                 parag.css('display', 'block');
                 $('#' + id).css('display', 'none');
             }
         }       
    });    
    calendar.render();
};



