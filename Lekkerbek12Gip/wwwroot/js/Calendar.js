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
            console.log(e.date.toISOString().slice(0, 10))
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
         }
     }
     calendar.on('dateClick', function (info) {         
         var ob = $('#' + info.dateStr);       
         const parag = $('#Create');
         var dvs = $('#index> div'); 
         if (true) {
             $('#Message').html("<h2>asd</>");
         }
         else { $('#Message').css("background-color:black") };
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



