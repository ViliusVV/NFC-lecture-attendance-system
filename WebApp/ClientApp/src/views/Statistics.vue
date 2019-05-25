<template>
<v-layout>
    <div style="margin-left: -8%">
  <app-diagram1>
    <v-flex xs12 sm4 offset-sm3>
      <v-card width="450"   class="elevation-4">
        <v-card-title primary-title>
            <h3 align="center" class="headline mb-0">Bendras lankomumas</h3>
        </v-card-title>
          <div>  
            <apexchart type=radialBar height=350 :options="chartOptions" :series="series" /> 
          </div>
      </v-card>
    </v-flex>
  </app-diagram1>
    </div>
  <div style="margin-left: 10%">
      <app-slide>
              <v-expansion-panel
        v-model="panel"
        expand
      >
            <v-expansion-panel-content
        v-for="(item,i) in 1"
        :key="i"
      >
        <template v-slot:header>
          <div>Laboratorinai darbai</div>
        </template>
      <v-layout wrap>
        <div style="margin-left: 5%" v-for="item in labAttendance" v-bind:key="item.title + item.type" >
           <v-progress-circular style="margin-left: 5%"
                :size="130"
                :value="item.attendance"
                color="green"
                :width="10"
            >{{item.title}} {{item.attendance}}%</v-progress-circular>
            &nbsp;
        </div>
      </v-layout>
      </v-expansion-panel-content>
            <v-expansion-panel-content
        v-for="(item,i) in 1"
        :key="i"
      >
        <template v-slot:header>
          <div>Teorinės paskaitos</div>
        </template>
      <v-layout wrap>
        <div style="margin-left: 5%" v-for="item in TeorAttendance" v-bind:key="item.title + item.type" >
           <v-progress-circular style="margin-left: 5%"
                :size="130"
                :value="item.attendance"
                color="red"
                :width="10"
            >{{item.title}} {{item.attendance}}%</v-progress-circular>
            &nbsp;
        </div>
      </v-layout>
      </v-expansion-panel-content>

                  <v-expansion-panel-content
        v-for="(item,i) in 1"
        :key="i"
      >
        <template v-slot:header>
          <div>Praktiniai užsiėmimai</div>
        </template>
      <v-layout wrap>
        <div style="margin-left: 5%" v-for="item in PrakAttendance" v-bind:key="item.title + item.type" >
           <v-progress-circular style="margin-left: 5%"
                :size="130"
                :value="item.attendance"
                color="blue"
                :width="10"
            >{{item.title}} {{item.attendance}}%</v-progress-circular>
            &nbsp;
        </div>
      </v-layout>
      </v-expansion-panel-content>
    </v-expansion-panel>
  </app-slide>
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


function getLaboraiAttendance(duom){
  var totallectures = 0;
  var totalattended = 0;
  duom.forEach(element => {
    if (element.type=="laborai") {
      totallectures = totallectures + element.total;
      totalattended = totalattended + element.attended;
    }
  });
  return (totalattended/totallectures*100).toFixed(2);
}

function getTeorijaAttendance(duom){
  var totallectures = 0;
  var totalattended = 0;
  duom.forEach(element => {
    if (element.type=="teorija") {
      totallectures = totallectures + element.total;
      totalattended = totalattended + element.attended;
    }
  });
  return (totalattended/totallectures*100).toFixed(2);
}

function getPraktikaAttendance(duom){
  var totallectures = 0;
  var totalattended = 0;
  duom.forEach(element => {
    if (element.type=="praktika") {
      totallectures = totallectures + element.total;
      totalattended = totalattended + element.attended;
    }
  });
  return (totalattended/totallectures*100).toFixed(2);
}

function getTotalAttendance(duom){
  var totallectures = 0;
  var totalattended = 0;
  duom.forEach(element => {
      totallectures = totallectures + element.total;
      totalattended = totalattended + element.attended;
  });
  return (totalattended/totallectures*100).toFixed(2);
}

var mas = [];

mas.push(getTotalAttendance(duom));
mas.push(getLaboraiAttendance(duom));
mas.push(getPraktikaAttendance(duom));
mas.push(getTeorijaAttendance(duom));


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
      labAttendance: [],
      TeorAttendance: [],
      PrakAttendance: [],
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
              colors: ['#1ab7ea', '#0084ff', '#39539E', '#0077B5'],
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
    created() {
      // iskvieciu zemiau apsirasyta metoda ir jam paduodu duomenu masyva kuri apsirasiau auksciau
      this.getData(lab, teor, prak);
    },
    methods: {
      // metodas, kuris pakeicia state objekto reiksme i paduodama per argumenta
      getData(labr, teori, prakt) {
        this.labAttendance = labr;
        this.TeorAttendance = teori;
        this.PrakAttendance = prakt;
      }
    }
};
</script>