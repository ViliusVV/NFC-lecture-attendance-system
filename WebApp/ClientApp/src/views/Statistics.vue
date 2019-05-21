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
      <v-layout column>
        <div style="margin-left: 5%">       
            <v-progress-circular
                :size="130"
                :value="100"
                color="blue"
                :width="10"
            >Algoritmų sudarymas ir analizė 100%</v-progress-circular>
&nbsp;
            <v-progress-circular
                :size="130"
                :value="80"
                color="deep-orange lighten-2"
                :width="10"
            >Programu sistemu inzinerija 80%</v-progress-circular>
&nbsp;
            <v-progress-circular
                :size="130"
                :value="70"
                color="brown"
                :width="10"
            >Semestro projektas 70%</v-progress-circular>
&nbsp;
            <v-progress-circular
                :size="130"
                :value="90"
                color="lime"
                :width="10"
            >Operacinės sistemos 90%</v-progress-circular>
        &nbsp;
            <v-progress-circular
                :size="130"
                :value="20"
                color="indigo darken-2"
                :width="10"
            >Duomenų bazės 20%</v-progress-circular>
            &nbsp;
            <v-progress-circular
                :size="130"
                :value="60"
                color="deep-orange lighten-2"
                :width="10"
            >dar vienas modulis 60%</v-progress-circular>
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
      <v-layout column>
        <div style="margin-left: 5%">       
            <v-progress-circular
                :size="130"
                :value="30"
                color="blue"
                :width="10"
            >Algoritmų sudarymas ir analizė 30%</v-progress-circular>
&nbsp;
            <v-progress-circular
                :size="130"
                :value="55"
                color="deep-orange lighten-2"
                :width="10"
            >Programu sistemu inzinerija 55%</v-progress-circular>
&nbsp;
            <v-progress-circular
                :size="130"
                :value="10"
                color="brown"
                :width="10"
            >Semestro projektas 10%</v-progress-circular>
&nbsp;
            <v-progress-circular
                :size="130"
                :value="87"
                color="lime"
                :width="10"
            >Operacinės sistemos 87%</v-progress-circular>
        &nbsp;
            <v-progress-circular
                :size="130"
                :value="25"
                color="indigo darken-2"
                :width="10"
            >Duomenų bazės 25%</v-progress-circular>
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
      <v-layout column>
        <div style="margin-left: 5%">       
            <v-progress-circular
                :size="130"
                :value="93"
                color="blue"
                :width="10"
            >Algoritmų sudarymas ir analizė 93%</v-progress-circular>
            &nbsp;
            <v-progress-circular
                :size="130"
                :value="70"
                color="deep-orange lighten-2"
                :width="10"
            >Programu sistemu inzinerija 70%</v-progress-circular>
            &nbsp;
            <v-progress-circular
                :size="130"
                :value="66"
                color="brown"
                :width="10"
            >Semestro projektas 66%</v-progress-circular>
            &nbsp;
            <v-progress-circular
                :size="130"
                :value="90"
                color="lime"
                :width="10"
            >Operacinės sistemos 90%</v-progress-circular>
            &nbsp;
            <v-progress-circular
                :size="130"
                :value="20"
                color="indigo darken-2"
                :width="10"
            >Duomenų bazės 20%</v-progress-circular>
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
import VueApexCharts from 'vue-apexcharts'

Vue.component('apexchart', VueApexCharts)

export default({
  data: function() {
    return {
      series: [50, 100, 100, 10],
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
          fontSize: '16px',
          position: 'left',
          offsetX: 35,
          offsetY: 10,
          labels: {
              useSeriesColors: true,
          },
          markers: {
              size: 0
          },
          formatter: function(seriesName, opts) {
              return seriesName + ":  " + opts.w.globals.series[opts.seriesIndex] + "%"
          },
          itemMargin: {
              horizontal: 1,
          }
        },
        responsive: [{
              breakpoint: 480,
              options: {
                  legend: {
                      show: false
                  }
              }
          }]
      }
    }
  }
});

</script>
