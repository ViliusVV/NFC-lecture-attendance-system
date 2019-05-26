<template>
  <v-container fluid>
    <v-layout row>
      <v-flex pa-2 >
        <v-card>
          <v-tooltip bottom>
            <template v-slot:activator="{ on }">
              <div v-on="on">
                <v-autocomplete
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
        </v-card>
      </v-flex>
    </v-layout >


    <v-layout offset-xs2 pa-2 row >
      <v-flex>
          <v-card  class="elevation-4">
          <v-card-title primary-title>
            <h3 align="center" class="headline mb-0">Bendras lankomumas</h3>
          </v-card-title>
          <div>
            <apexchart type="radialBar" height="350" :options="chartOptions" :series="series"/>
          </div>
        </v-card>
      </v-flex>
    </v-layout>

    <v-layout pa-2 row>
      <v-flex>
        <v-expansion-panel expand>
        <v-expansion-panel-content v-for="(item,i) in 1" :key="i">
          <template v-slot:header>
            <div>Laboratorinai darbai</div>
          </template>
          <v-layout wrap>
            <div
              style="margin-left: 5%"
              v-for="item in attendanceLab"
              v-bind:key="item.title"
            >
              <v-progress-circular
                style="margin-left: 5%"
                :size="180"
                :value="item.attendance"
                color="green"
                :width="15"
              >{{item.title}} {{item.attendance}}%</v-progress-circular>&nbsp;
            </div>
          </v-layout>
        </v-expansion-panel-content>
        <v-expansion-panel-content v-for="(item,i) in 1" :key="i">
          <template v-slot:header>
            <div>Teorinės paskaitos</div>
          </template>
          <v-layout wrap>
            <div
              style="margin-left: 5%"
              v-for="item in attendanceTheory"
              v-bind:key="item.title"
            >
              <v-progress-circular
                style="margin-left: 5%"
                :size="180"
                :value="item.attendance"
                color="red"
                :width="15"
              >{{item.title}} {{item.attendance}}%</v-progress-circular>&nbsp;
            </div>
          </v-layout>
        </v-expansion-panel-content>

        <v-expansion-panel-content v-for="(item,i) in 1" :key="i">
          <template v-slot:header>
            <div>Praktiniai užsiėmimai</div>
          </template>
          <v-layout wrap>
            <div
              style="margin-left: 5%"
              v-for="item in attendancePractice"
              v-bind:key="item.title"
            >
              <v-progress-circular
                style="margin-left: 5%"
                :size="180"
                :value="item.attendance"
                color="blue"
                :width="15"
              >{{item.title}} {{item.attendance}}%</v-progress-circular>&nbsp;
            </div>
          </v-layout>
        </v-expansion-panel-content>
      </v-expansion-panel>
      </v-flex>
    </v-layout>
  </v-container>
</template>



<script lang="js">
import { Component, Vue } from 'vue-property-decorator';
import axios from 'axios';
import '../components/login/auth-header';
import { authHeader } from '../components/login/auth-header';
import VueApexCharts from 'vue-apexcharts';

// Apsirasau duomenis
const duom = [
  {title:"Duomenų bazės",type:"teorija",attended:14,total:16,attendance:87.5},
  {title:"Duomenų bazės",type:"laborai",attended:12,total:16,attendance:75},
  {title:"Duomenų bazės",type:"praktika",attended:5,total:16,attendance:31.25},
  {title:"Algoritmų sudarymas ir analizė",type:"teorija",attended:8,total:16,attendance:50},
  {title:"Algoritmų sudarymas ir analizė",type:"laborai",attended:6,total:16,attendance:37.5},
  {title:"Algoritmų sudarymas ir analizė",type:"praktika",attended:14,total:16,attendance:87.5},
  {title:"Semestro projektas",type:"laborai",attended:5,total:16,attendance:31.25},
  {title:"Operacinės sistemos",type:"teorija",attended:14,total:16,attendance:87.5},
  {title:"Operacinės sistemos",type:"laborai",attended:15,total:16,attendance:93.75}
];

var mas = [];


var teor = [];
var lab = [];
var prak = [];

function getRequiredLectureType(duom){
  duom.forEach(element => {
      if (element.type == "teorija") {
        teor.push(element);
      }
      if (element.type == "laborai") {
        lab.push(element);
      } 
      if (element.type == "praktika") {
        prak.push(element);
      }
  });
  return 1;
}

var notneeded = getRequiredLectureType(duom);

Vue.component('apexchart', VueApexCharts)
export default {
    data: () => ({
      loading: false,
      items: [],
      search: null,
      select: null,
      select: null,
      fetched: false,

      attendanceLab: [],
      attendancePractice: [],
      attendanceTheory: [],
      series: mas,
            chartOptions: {
              plotOptions: {
                radialBar: {
                  offsetY: -10,
                  startAngle: 0,
                  endAngle: 270,
                  hollow: {
                    margin: 3,
                    size: '30%',
                    background: 'transparent',
                    image: undefined,
                  },
                  dataLabels: {
                    name: {
                      show: false,
                    },
                    value: {
                      show: true,
                    }
                  }
                }
              },
              colors: ['#f9a825', '#388e3c', '#3949ab', '#d50000'],
              labels: ['Iš viso', 'Laboratorinai', 'Pratybos', 'Teorija'],
              legend: {
                show: true,
                floating: true,
                fontSize: '15px',
                position: 'left',
                offsetX: 25,
                offsetY: 10,
                labels: {
                    useSeriesColors: true,
                },
                markers: {
                    size: 0,
                },
                formatter: function(seriesName, opts) {
                    return seriesName + ':  ' + opts.w.globals.series[opts.seriesIndex] + '%';
                },
                itemMargin: {
                    horizontal: 1,
                }
              },
              responsive: [{
                    breakpoint: 480,
                    options: {
                        legend: {
                            show: false,
                        }
                    }
                }]
            }
    }),
    watch: {
      search (val) {
        val && val !== this.select && this.querySelections(val);

        if(this.select !== null) {
          //if student is selected, gets his VIDKO code from input
          const studentCode = this.select.substring(0, this.select.indexOf(" "));
          //then gets his id, by his VIDKO code
          axios.get(`api/userlist/getuserid/${studentCode}`)
            .then(response => {
                const headers = { ...authHeader() };
                axios.get("/api/stats/GetStudentStatTotal/" + response.data ,{ headers: headers})
                  .then(response => {
                    this.updateTotalAttendance(response["data"]);
                })
                
                axios.get("/api/stats/GetStudentStat/" + response.data ,{ headers: headers})
                  .then(response => {
                    this.updateAttendance(response["data"])
                  })
            this.fetched = true;
            });
        }
      }
    },
    // sitas metodas iskvieciamas viena karta kai view yra sukuriamas
    methods: {
      // metodas, kuris pakeicia state objekto reiksme i paduodama per argumenta
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
      updateAttendance(attendanceArr)
      {
        console.log(attendanceArr);
        this.attendanceLab.length = 0;
        this.attendancePractice.length = 0;
        this.attendanceTheory.length = 0;
        attendanceArr.forEach(element =>
        {
          var objP = 
          {
            title : element.lectureID + " " + element.lectureName,
            attendance : element.attendancePractice,
          }
          var objL =
          {
            title : element.lectureID + " " + element.lectureName,
            attendance : element.attendanceLab,
          } 
          var objT = 
          {
            title : element.lectureID + " " + element.lectureName,
            attendance : element.attendanceTheory,
          }
          if(element.totalLab > 0){
            this.attendanceLab.push(objL);
          }
          if(element.totalPractice > 0){
            this.attendancePractice.push(objP);
          }
          if(element.totalTheory > 0){
            this.attendanceTheory.push(objT);
          }
        })
        return;
      },
      updateTotalAttendance(attendance)
      {
          this.series.length = 0;
          var totalAttendance = (attendance["visitedLab"] + attendance["visitedPractice"] +attendance["visitedTheory"])  * 100.0
                           / (attendance["totalLab"] + attendance["totalPractice"] + attendance["totalTheory"]);
          this.series.push(totalAttendance.toFixed(1));
          this.series.push(attendance["attendanceLab"]);
          this.series.push(attendance["attendancePractice"]);
          this.series.push(attendance["attendanceTheory"]);
          return;
      }
    },
    created() {
      const headers = { ...authHeader() };
      var userId = JSON.parse(localStorage.getItem("user"))["userName"]["id"];
      axios.get("/api/stats/GetStudentStatTotal/" + userId ,{ headers: headers})
        .then(response => {
          this.updateTotalAttendance(response["data"]);
      })
      
      axios.get("/api/stats/GetStudentStat/" + userId ,{ headers: headers})
        .then(response => {
          this.updateAttendance(response["data"])
        })
    },
};
</script>