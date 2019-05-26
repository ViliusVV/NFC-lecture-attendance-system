<template>
  <v-layout>
    <div style="margin-left: 1%; margin-top: 1%; float:left; clear:left">
        <v-card width="600" class="elevation-4">
          <v-card-title primary-title>
            <h3 align="center" class="headline mb-0">Bendras lankomumas</h3>
          </v-card-title>
          <div>
            <apexchart type="radialBar" height="350" :options="chartOptions" :series="series"/>
          </div>
        </v-card>
    </div>
    <div style="margin-left: 1%; margin-top: 1%">
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
                :size="150"
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
                :size="150"
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
                :size="150"
                :value="item.attendance"
                color="blue"
                :width="15"
              >{{item.title}} {{item.attendance}}%</v-progress-circular>&nbsp;
            </div>
          </v-layout>
        </v-expansion-panel-content>
      </v-expansion-panel>
    </div>
  </v-layout>
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
      // Susikuriu data, dar kitaip vadinama state objekta, i kuri desiu visus paskaitus objektus
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
                    margin: 5,
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
    // sitas metodas iskvieciamas viena karta kai view yra sukuriamas
    methods: {
      // metodas, kuris pakeicia state objekto reiksme i paduodama per argumenta
      getData(labr, teori, prakt) {
        this.labAttendance = labr;
        this.TeorAttendance = teori;
        this.PrakAttendance = prakt;
      },
      updateAttendance(attendanceArr)
      {
        console.log(attendanceArr);
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