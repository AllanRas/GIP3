$(async() => {   
    const resp = await fetch('/PlanningsModules/Event');
    const event = await resp.json();
    console.log(event);
    calender(event);
         
})
 function calender(events) {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        events: events
    });
    calendar.on('dateClick', function (info) {
        var ob = $('#'+info.dateStr);
        ob.css('background-color', 'cyan');
        console.log(info.dateStr);
        });
    calendar.render();
};



