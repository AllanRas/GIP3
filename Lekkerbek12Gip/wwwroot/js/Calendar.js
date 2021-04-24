$(async() => {   
    const resp = await fetch('/PlanningsModules/Event');
    const event = await resp.json();
    const dates = await fetch('/PlanningsModules/Warning')
    const datesres = await dates.json();
    const d = datesres.map(x => x.slice(0, 10));

    calender(event,d);
   
         
})
function calender(events,datesarr) {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        events: events,
        //editable: false,
        dayCellContent: function (e) {
            (e.date.setDate(e.date.getDate() + 1));            
            for (var i in datesarr) {
                if (e.date.toISOString().slice(0, 10) == datesarr[i]) {
                    e.dayNumberText = { html: `<p><i class= "fas fa-exclamation-circle" style="font-size:24px;color:red;margin-right:10px" ></i>${e.dayNumberText}</p>` }
                }

            }
        }
    });

     var todayDate = new Date().toISOString().slice(0, 10);
     var dvs = $('#index> div');
     for (var i in dvs) {
         const id = dvs[i].id;
         if (id == todayDate) {
             $('#' + id).css('display', 'block');
             $('#Create').css('display', 'none');
         }         
     }
     calendar.on('dateClick', function (info) {         
         var ob = $('#' + info.dateStr);       
         const parag = $('#Create');
         var dvs = $('#index> div'); 

         let isParag = false;
         for (var i in dvs) {            
             const id = dvs[i].id;
              
             if (id == info.dateStr){               
                 $('#' + id).css('display', 'block');
                 isParag = true;               
             }
             else
             {          
                 $('#' + id).css('display', 'none');
             }             
         }
         isParag ? parag.css('display', 'none') : parag.css('display', 'block');      
    });    
    calendar.render();
};



