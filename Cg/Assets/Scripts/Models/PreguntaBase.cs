using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace models
{
    public abstract class PreguntaBase
    {
        public abstract string Pregunta { get; set; }
        public abstract string RespuestaCorrecta { get; set; }
    }
}
