<template>
  <v-layout>
    <v-flex
      sm12
      lg3
      class="pa-3 mb-3 feature-pane"
    >
      <v-btn
        fab
        outline
        small
        absolute
        left
        color="primary"
        @click="$refs.calendar.prev()"
      >
        <v-icon dark>
          keyboard_arrow_left
        </v-icon>
      </v-btn>
      <v-btn
        fab
        outline
        small
        absolute
        right
        color="primary"
        @click="$refs.calendar.next()"
      >
        <v-icon
          dark
        >
          keyboard_arrow_right
        </v-icon>
      </v-btn>
      <br><br><br>
      <v-select
        v-model="type"
        :items="typeOptions"
        label="Kalendoriaus tipas"
      ></v-select>
      <v-menu
        ref="startMenu"
        v-model="startMenu"
        :close-on-content-click="false"
        :nudge-right="40"
        :return-value.sync="start"
        transition="scale-transition"
        min-width="290px"
        lazy
        offset-y
        full-width
      >
        <template v-slot:activator="{ on }">
          <v-text-field
            v-model="start"
            label="Data"
            prepend-icon="event"
            readonly
            v-on="on"
          ></v-text-field>
        </template>
        <v-date-picker
          v-model="start"
          locale="lt"
          no-title
          scrollable
        >
          <v-spacer></v-spacer>
          <v-btn
            flat
            color="primary"
            @click="startMenu = false"
          >
            Cancel
          </v-btn>
          <v-btn
            flat
            color="primary"
            @click="$refs.startMenu.save(start)"
          >
            OK
          </v-btn>
        </v-date-picker>
      </v-menu>
      <v-menu
        v-if="hasEnd"
        ref="endMenu"
        v-model="endMenu"
        :close-on-content-click="false"
        :nudge-right="40"
        :return-value.sync="end"
        transition="scale-transition"
        min-width="290px"
        lazy
        offset-y
        full-width
      >
        <template v-slot:activator="{ on }">
          <v-text-field
            v-model="end"
            label="End Date"
            prepend-icon="event"
            readonly
            v-on="on"
          ></v-text-field>
        </template>
        <v-date-picker
          v-model="end"
          no-title
          scrollable
        >
          <v-spacer></v-spacer>
          <v-btn
            flat
            color="primary"
            @click="endMenu = false"
          >
            Cancel
          </v-btn>
          <v-btn
            flat
            color="primary"
            @click="$refs.endMenu.save(end)"
          >
            OK
          </v-btn>
        </v-date-picker>
      </v-menu>
      <v-menu
        ref="nowMenu"
        v-model="nowMenu"
        :close-on-content-click="false"
        :nudge-right="40"
        :return-value.sync="now"
        transition="scale-transition"
        min-width="290px"
        lazy
        offset-y
        full-width
      >
        <v-spacer></v-spacer>
        <v-btn
          flat
          color="primary"
          @click="nowMenu = false"
        >
          Cancel
        </v-btn>
        <v-btn
          flat
          color="primary"
          @click="$refs.nowMenu.save(now)"
        >
          OK
          </v-btn>
      </v-menu>
      <v-select
        v-model="weekdays"
        :items="weekdaysOptions"
        label="Dienų intervalai"
      ></v-select>
      <v-tooltip bottom>
          <template v-slot:activator="{ on }">
            <div v-on="on">
            <v-autocomplete
              v-if="isAdminOrLecturer"
              v-model="select"              
              :loading="loading"
              :items="items"
              :search-input.sync="search"
              cache-items
              flat
              hide-no-data
              hide-details
              label="Įveskite studento VIDKO arba vardą ir pavardę"
            ></v-autocomplete> 
            </div>             
          </template>
        <span>Įveskite studento VIDKO arba vardą ir pavardę</span>
      </v-tooltip>
      <v-text-field
        v-if="type === 'custom-weekly'"
        v-model="minWeeks"
        label="Minimum Weeks"
        type="number"
      ></v-text-field>
      <v-select
        v-if="type === 'custom-daily'"
        v-model="maxDays"
        :items="maxDaysOptions"
        label="# of Days"
      ></v-select>
    </v-flex>
    <v-flex
      sm12
      lg9
      class="pl-3 mt-2"
      v-if="fetched"
    >
      <v-sheet height="85vh">
        <v-progress-circular
          v-if="events.length == 0"
          :size="70"
          :width="7"
          color="primary"
          indeterminate
          style="position: absolute; z-index: 99; left:45.5%; top: 43.2%"
        ></v-progress-circular>
        <v-calendar
          locale="lt"
          ref="calendar"
          v-model="start"
          :type="type"
          :start="start"
          :end="end"
          :min-weeks="minWeeks"
          :max-days="maxDays"
          :now="now"
          :dark="dark"
          :weekdays="weekdays"
          :first-interval="intervals.first"
          :interval-minutes="intervals.minutes"
          :interval-count="intervals.count"
          :interval-height="intervals.height"
          :interval-style="intervalStyle"
          :show-interval-label="showIntervalLabel"
          :color="color"
        >
        <template v-slot:dayBody="{ date, timeToY, minutesToPixels }">
            <template v-for="event in eventsMap[date]">
              <!-- timed events -->
              <div
                :key="event.key"
                :style="{ top: timeToY(event.start.substring(11,16)) + 'px', height: minutesToPixels(event.lectureDuration) + 'px', color: 'black', backgroundColor: event.colorTimed, borderColor: event.colorTimed}"
                class="my-eventTime with-time"
                @click="open(event)"
              >
                <div v-if="event.isVisited">
                  <v-icon color="green darken-2">check_circle_outline</v-icon>
                  {{event.title}}
                </div>
                <div v-if="!event.isVisited">
                  <v-icon color="red darken-2">highlight_off</v-icon>
                  {{event.title}}
                </div>
              </div>
            </template>
          </template>
        <template v-slot:day="{ date }">
              <template v-for="event in eventsMap[date]">
                <div :key="event.key">   
                  <v-sheet :color="event.color">             
                    <v-menu
                      :key="event.key"
                      v-model="event.open"
                      full-width
                      offset-x
                    >                
                      <template v-slot:activator="{ on }">
                        <div
                          class="my-event"
                          style="margin-bottom: 2px"
                          v-on="on"                             
                        >
                          <div v-if="!event.isVisited">
                            <v-icon color="red darken-2">highlight_off</v-icon>
                            {{event.title}}
                          </div>
                          <div v-if="event.isVisited">
                            <v-icon color="green darken-2">check_circle_outline</v-icon>
                            {{event.title}}
                          </div>
                        </div>
                      </template>
                      <v-card
                        color="grey lighten-4"
                        min-width="350px"
                        flat
                      >
                        <v-toolbar
                          :color="event.color"
                          dark
                        >
                          <v-toolbar-title v-html="event.title"></v-toolbar-title>
                          <v-spacer></v-spacer>
                        </v-toolbar>
                        <v-card-title primary-title>
                          <span v-html="event.details"></span>
                        </v-card-title>
                        <v-card-actions>
                          <v-btn
                            flat
                            color="secondary"
                          >
                            Close
                          </v-btn>
                        </v-card-actions>
                      </v-card>
                    </v-menu>
                  </v-sheet>
                </div>
              </template>
            </template>
        </v-calendar>
      </v-sheet>
    </v-flex>
  </v-layout>
</template>

<script>
  const weekdaysDefault = [1, 2, 3, 4, 5, 6, 0]
  const intervalsDefault = {
    first: 0,
    minutes: 60,
    count: 24,
    height: 40
  }
  const stylings = {
    default (interval) {
      return undefined
    },
    workday (interval) {
      const inactive = interval.weekday === 0 ||
        interval.weekday === 6 ||
        interval.hour < 9 ||
        interval.hour >= 17
      const startOfHour = interval.minute === 0
      const dark = this.dark
      const mid = dark ? 'rgba(255,255,255,0.1)' : 'rgba(0,0,0,0.1)'
      return {
        backgroundColor: inactive ? (dark ? 'rgba(0,0,0,0.4)' : 'rgba(0,0,0,0.05)') : undefined,
        borderTop: startOfHour ? undefined : '1px dashed ' + mid
      }
    },
    past (interval) {
      return {
        backgroundColor: interval.past ? (this.dark ? 'rgba(0,0,0,0.4)' : 'rgba(0,0,0,0.05)') : undefined
      }
    }
  }
  export default {
    data: () => ({
      dark: false,
      startMenu: false,
      start: new Date().toJSON().slice(0,10),
      endMenu: false,
      end: '2019-12-25',
      nowMenu: false,
      minWeeks: 1,
      now: null,
      type: 'month',
      typeOptions: [
        { text: 'Dieninis', value: 'day' },
        { text: 'Savaitinis', value: 'week' },
        { text: 'Mėnesinis', value: 'month' },
      ],
      weekdays: weekdaysDefault,
      weekdaysOptions: [
        { text: 'Pr - Pn', value: [1, 2, 3, 4, 5] },
        { text: 'Pr - Sk', value: [1, 2, 3, 4, 5, 6, 0] }
      ],
      intervals: intervalsDefault,
      intervalsOptions: [
        { text: 'Default', value: intervalsDefault },
        { text: 'Workday', value: { first: 16, minutes: 30, count: 20, height: 40 } }
      ],
      maxDays: 7,
      maxDaysOptions: [
        { text: '7 days', value: 7 },
        { text: '5 days', value: 5 },
        { text: '4 days', value: 4 },
        { text: '3 days', value: 3 }
      ],
      styleInterval: 'default',
      styleIntervalOptions: [
        { text: 'Default', value: 'default' },
        { text: 'Workday', value: 'workday' },
        { text: 'Past', value: 'past' }
      ],
      color: 'primary',
      colorOptions: [
        { text: 'Primary', value: 'primary' },       
        { text: 'Red', value: 'red' },       
        { text: 'Blue', value: 'blue' },       
        { text: 'Green', value: 'green' },        
      ],
      events: [],
      loading: false,
      items: [],
      search: null,
      select: null,
      isAdminOrLecturer: false,
      fetched: false,
    }),
    computed: {
      intervalStyle () {
        return stylings[ this.styleInterval ].bind(this)
      },
      hasIntervals () {
        return this.type in {
          'week': 1, 'day': 1, '4day': 1, 'custom-daily': 1
        }
      },
      hasEnd () {
        return this.type in {
          'custom-weekly': 1, 'custom-daily': 1
        }
      },
        eventsMap () {
        const map = {}
        this.events.forEach(e => (map[e.date] = map[e.date] || []).push(e))
        return map
      }
    },
    created () {
      const userData = JSON.parse(localStorage.getItem("user"));
      const userId =  userData.userName.id;      
      this.fetchLectures(`api/lectures/${userId}`);
      
      if(userData.role.roleId == 'ADMIN' || userData.role.roleId == 'LECTURER') {
        this.isAdminOrLecturer = true;
      } else {
        this.fetched = true;
      }
   },
   watch: {
      search (val) {
        val && val !== this.select && this.querySelections(val);

        if(this.select !== null) {
          //if student is selected, gets his VIDKO code from input
          const studentCode = this.select.substring(0, this.select.indexOf(" "));
          //then gets his id, by his VIDKO code
          axios.get(`api/userlist/getuserid/${studentCode}`)
            .then(response => {
              //and finally gets his lectures
              this.fetchLectures(`api/lectures/${response.data}`);
              this.fetched = true;
            });
        }
      }
    },
    methods: {
      showIntervalLabel (interval) {
        return interval.minute === 0
      },
      open (event) {
        //Should show lecture detailed lecture data in weekly/daily calendar
      },
      querySelections (v) {
        this.loading = true
        axios.get(`api/userlist/getusers`)
        .then(response => {
          const users = response.data
          users.forEach(element => {
            this.items.push(element.studentCode + " - " + element.name + " " + element.surname)
          })
          this.loading = false
        })
      },
      fetchLectures(serviceUrl) {
        axios.get(serviceUrl)
        .then(response => {
          // JSON responses are automatically parsed.
          this.events = response.data
          this.events.forEach(element => {
            element.key = Math.random();
            element.date = element.start.substring(0,10);
            if (element.details.includes('Teori')) {
              element.color = 'red lighten-3'
              element.colorTimed = '#EF9A9A';
            } else if (element.details.includes('Labor')) {
              element.color = 'green lighten-3';
              element.colorTimed = '#A5D6A7';
            } else {
              element.color = 'blue lighten-3'
              element.colorTimed = '#90CAF9';
            }
            element.lectureDuration = Math.floor((Math.abs(new Date(element.start) - new Date(element.finish))/1000)/60);
            element.open = false;
          });
        })
        .catch(e => {
          this.errors.push(e)
        })
      }
    }
  }
  
</script>


<style lang="stylus" scoped>
  .feature-pane {
    position: relative;
    padding-top: 30px;
    box-shadow: 0 0 10px rgba(0,0,0,0.3);
    height: 85vh;
  }
  .day-header {
    margin: 0px 2px 2px 2px;
    padding: 2px 6px;
    background-color: #1867c0;
    color: #ffffff;
    border: 1px solid #1867c0;
    border-radius: 2px;
    user-select: none;
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
  }
  .day-body {
    position: absolute;
    top: 400px;
    height: 36px;
    margin: 2px;
    padding: 2px 6px;
    background-color: #1867c0;
    color: #ffffff;
    border: 1px solid #1867c0;
    border-radius: 2px;
    left: 0;
    right: 0;
    user-select: none;
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
  }
  .day {
    position: relative;
    height: 24px;
    margin: 0px;
    padding: 0px 6px;
    background-color: #1867c0;
    color: #ffffff;
    border: 1px solid #1867c0;
    border-radius: 2px;
    left: 0;
    right: 0;
    user-select: none;
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
  }
  .my-eventTime {
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    border-radius: 2px;
    color: #ffffff;
    font-size: 12px;
    padding: 3px;
    cursor: pointer;
    margin-bottom: 1px;
    left: 4px;
    margin-right: 8px;
    position: relative;

    &.with-time {
      position: absolute;
      right: 4px;
      margin-right: 0px;
    }
  }
</style>