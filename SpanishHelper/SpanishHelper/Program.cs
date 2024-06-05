using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpanishQuiz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Spanish Quiz!");

            List<Question> questions = GenerateQuestions();

            // Shuffle the questions to mix them up
            Shuffle(questions);

            int score = 0;
            int totalQuestions = 0;
            List<string> sessionAnswers = new List<string>();

            foreach (Question question in questions)
            {
                totalQuestions++;
                Console.WriteLine(question.Prompt);

                // Shuffle the options to mix them up
                Shuffle(question.Options);

                // Display multiple-choice options
                for (int i = 0; i < question.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Options[i]}");
                }

                Console.Write("Your answer: ");
                int userChoice = int.Parse(Console.ReadLine());

                string userAnswer = question.Options[userChoice - 1];
                sessionAnswers.Add(userAnswer);

                if (userAnswer == question.Answer)
                {
                    Console.WriteLine("Correct!\n");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Incorrect. The correct answer is: {question.Answer}\n");
                }

                Console.WriteLine($"Current Score: {score}/{totalQuestions}\n");

                // End session after every 10 questions
                if (totalQuestions % 10 == 0)
                {
                    Console.WriteLine("Do you want to continue the session? (yes/no)");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "no")
                    {
                        EndSession(score, totalQuestions, sessionAnswers);
                        return;
                    }
                    else if (choice != "yes")
                    {
                        Console.WriteLine("Invalid choice. Session continues by default.");
                    }
                }
            }

            EndSession(score, totalQuestions, sessionAnswers);
            Console.ReadLine();
        }

        static List<Question> GenerateQuestions()
        {
            List<Question> questions = new List<Question>
    {
        // Vocabulary Questions from Set 1
        new Question("What is the Spanish word for 'clinic'?", new List<string> { "la clínica", "el hospital", "la farmacia", "el consultorio" }, "la clínica"),
        new Question("What is the Spanish word for 'doctor's office'?", new List<string> { "el consultorio", "la clínica", "el hospital", "la farmacia" }, "el consultorio"),
        new Question("What is the Spanish word for 'dentist'?", new List<string> { "el / la dentista", "el / la doctor/a", "el / la enfermero/a", "el / la cirujano/a" }, "el / la dentista"),
        new Question("What is the Spanish word for 'pharmacy'?", new List<string> { "la farmacia", "la clínica", "el hospital", "el consultorio" }, "la farmacia"),
        new Question("What is the Spanish word for 'operation, surgery'?", new List<string> { "la operación", "el examen médico", "la sala de emergencia", "el accidente" }, "la operación"),
        new Question("What is the Spanish word for 'emergency room'?", new List<string> { "la sala de emergencia (s)", "el hospital", "la farmacia", "el consultorio" }, "la sala de emergencia (s)"),
        new Question("What is the Spanish word for 'body'?", new List<string> { "el cuerpo", "la cabeza", "el corazón", "el oído" }, "el cuerpo"),
        new Question("What is the Spanish word for '(sense of) hearing; inner ear'?", new List<string> { "el oído", "la oreja", "la boca", "el ojo" }, "el oído"),
        new Question("What is the Spanish word for 'accident'?", new List<string> { "el accidente", "la operación", "el síntoma", "la sala de emergencia" }, "el accidente"),
        new Question("What is the Spanish word for 'health'?", new List<string> { "la salud", "el síntoma", "la clínica", "el hospital" }, "la salud"),
        new Question("What is the Spanish word for 'symptom'?", new List<string> { "el síntoma", "la salud", "el cuerpo", "la farmacia" }, "el síntoma"),
        new Question("What is the Spanish word for 'to fall (down)'?", new List<string> { "caerse", "darse con", "doler", "enfermarse" }, "caerse"),
        new Question("What is the Spanish word for 'to bump into; to run into'?", new List<string> { "darse con", "caerse", "doler", "enfermarse" }, "darse con"),
        new Question("What is the Spanish word for 'to hurt'?", new List<string> { "doler (o:ue)", "lastimarse", "romperse", "sufrir" }, "doler (o:ue)"),
        new Question("What is the Spanish word for 'to get sick'?", new List<string> { "enfermarse", "estar enfermo", "lastimarse", "ponerse una inyección" }, "enfermarse"),
        new Question("What is the Spanish word for 'to be sick'?", new List<string> { "estar enfermo / a", "enfermarse", "lastimarse", "ponerse una inyección" }, "estar enfermo / a"),
        new Question("What is the Spanish word for 'to injure (one's foot)'?", new List<string> { "lastimarse (el pie)", "romperse (la pierna)", "sacar(se) un diente", "sufrir una enfermedad" }, "lastimarse (el pie)"),
        new Question("What is the Spanish word for 'to give an injection'?", new List<string> { "poner una inyección", "recetar", "toser", "romperse" }, "poner una inyección"),
        new Question("What is the Spanish word for 'to prescribe'?", new List<string> { "recetar", "poner una inyección", "romperse", "toser" }, "recetar"),
        new Question("What is the Spanish word for 'to break (one's leg)'?", new List<string> { "romperse (la pierna)", "lastimarse (el pie)", "toser", "recetar" }, "romperse (la pierna)"),
        new Question("What is the Spanish word for 'to have a tooth removed'?", new List<string> { "sacar(se) un diente", "romperse", "toser", "lastimarse" }, "sacar(se) un diente"),
        new Question("What is the Spanish word for 'to suffer an illness'?", new List<string> { "sufrir una enfermedad", "estar enfermo", "enfermarse", "lastimarse" }, "sufrir una enfermedad"),
        new Question("What is the Spanish word for 'to sprain (one's ankle)'?", new List<string> { "torcerse (o:ue) (el tobillo)", "romperse (la pierna)", "lastimarse (el pie)", "sacar(se) un diente" }, "torcerse (o:ue) (el tobillo)"),
        new Question("What is the Spanish word for 'to cough'?", new List<string> { "toser", "poner una inyección", "recetar", "caerse" }, "toser"),
        new Question("What is the Spanish word for 'heart'?", new List<string> { "el corazón", "la cabeza", "el cuerpo", "el oído" }, "el corazón"),
        new Question("What is the Spanish word for 'patient'?", new List<string> { "el paciente", "el doctor", "el enfermero", "el dentista" }, "el paciente"),
        new Question("What is the Spanish word for 'head'?", new List<string> { "la cabeza", "el cuerpo", "el corazón", "el oído" }, "la cabeza"),
        new Question("What is the Spanish word for 'eye'?", new List<string> { "el ojo", "la nariz", "la boca", "el oído" }, "el ojo"),
        new Question("What is the Spanish word for 'nose'?", new List<string> { "la nariz", "la boca", "el ojo", "el oído" }, "la nariz"),
        new Question("What is the Spanish word for 'doctor'?", new List<string> { "la doctora", "el paciente", "la enfermera", "el dentista" }, "la doctora"),
        new Question("What is the Spanish word for 'ear'?", new List<string> { "la oreja", "el oído", "la boca", "la cabeza" }, "la oreja"),
        new Question("What is the Spanish word for 'mouth'?", new List<string> { "la boca", "la nariz", "el ojo", "la oreja" }, "la boca"),
        new Question("What is the Spanish word for 'neck'?", new List<string> { "el cuello", "la garganta", "el estomago", "la rodilla" }, "el cuello"),
        new Question("What is the Spanish word for 'throat'?", new List<string> { "la garganta", "el cuello", "el estomago", "la rodilla" }, "la garganta"),
        new Question("What is the Spanish word for 'stomach'?", new List<string> { "el estomago", "la cabeza", "el corazón", "el cuerpo" }, "el estomago"),
        new Question("What is the Spanish word for 'finger'?", new List<string> { "el dedo", "la rodilla", "el tobillo", "el pie" }, "el dedo"),
        new Question("What is the Spanish word for 'knee'?", new List<string> { "la rodilla", "el pie", "el tobillo", "el dedo" }, "la rodilla"),
        new Question("What is the Spanish word for 'toe'?", new List<string> { "el dedo del pie", "el dedo", "la rodilla", "el pie" }, "el dedo del pie"),
        new Question("What is the Spanish word for 'x-ray'?", new List<string> { "la radiografía", "la operación", "el síntoma", "la sala de emergencia" }, "la radiografía"),
        new Question("What is the Spanish word for 'bone'?", new List<string> { "el hueso", "el cuerpo", "el corazón", "el oído" }, "el hueso"),
        new Question("What is the Spanish word for 'nurse'?", new List<string> { "la enfermera", "la doctora", "el paciente", "el dentista" }, "la enfermera"),
        new Question("What is the Spanish word for 'to sneeze'?", new List<string> { "estornudar", "toser", "recetar", "doler" }, "estornudar"),
        new Question("What is the Spanish word for 'to take a temperature'?", new List<string> { "tomar la temperatura", "poner una inyección", "recetar", "caerse" }, "tomar la temperatura"),
        new Question("What is the Spanish word for 'arm'?", new List<string> { "el brazo", "la pierna", "el tobillo", "el dedo" }, "el brazo"),
        new Question("What is the Spanish word for 'leg'?", new List<string> { "la pierna", "el brazo", "el tobillo", "el dedo" }, "la pierna"),
        new Question("What is the Spanish word for 'ankle'?", new List<string> { "el tobillo", "la pierna", "el brazo", "el dedo" }, "el tobillo"),
        new Question("What is the Spanish word for '(head)ache; pain'?", new List<string> { "el dolor (de cabeza)", "el síntoma", "la salud", "el accidente" }, "el dolor (de cabeza)"),
        new Question("What is the Spanish word for 'flu'?", new List<string> { "la gripe", "el resfriado", "la infección", "la tos" }, "la gripe"),
        new Question("What is the Spanish word for 'infection'?", new List<string> { "la infección", "el resfriado", "la gripe", "la tos" }, "la infección"),
        new Question("What is the Spanish word for 'cold'?", new List<string> { "el resfriado", "la gripe", "la tos", "la infección" }, "el resfriado"),
        new Question("What is the Spanish word for 'cough'?", new List<string> { "la tos", "la gripe", "el resfriado", "la infección" }, "la tos"),
        new Question("What is the Spanish word for 'congested'?", new List<string> { "congestionado / a", "mareado/a", "grave", "saludable" }, "congestionado / a"),
        new Question("What is the Spanish word for 'pregnant'?", new List<string> { "embarazada", "grave", "mareado/a", "saludable" }, "embarazada"),
        new Question("What is the Spanish word for 'grave; serious'?", new List<string> { "grave", "embarazada", "mareado/a", "saludable" }, "grave"),
        new Question("What is the Spanish word for 'dizzy; nauseated'?", new List<string> { "mareado/a", "grave", "embarazada", "saludable" }, "mareado/a"),
        new Question("What is the Spanish word for 'medical'?", new List<string> { "médico / a", "grave", "saludable", "sano" }, "médico / a"),
        new Question("What is the Spanish word for 'healthy'?", new List<string> { "saludable", "sano", "médico / a", "grave" }, "saludable"),
        new Question("What is the Spanish word for 'to be allergic (to)'?", new List<string> { "ser alérgico/a (a)", "tener dolor (m.)", "tener fiebre (f)", "sufrir una enfermedad" }, "ser alérgico/a (a)"),
        new Question("What is the Spanish word for 'to have pain'?", new List<string> { "tener dolor (m.)", "ser alérgico/a (a)", "tener fiebre (f)", "sufrir una enfermedad" }, "tener dolor (m.)"),
        new Question("What is the Spanish word for 'to have a fever'?", new List<string> { "tener fiebre (f)", "tener dolor (m.)", "ser alérgico/a (a)", "sufrir una enfermedad" }, "tener fiebre (f)"),
        new Question("What is the Spanish word for 'antibiotic'?", new List<string> { "el antibiótico", "la aspirina", "el medicamento", "la pastilla" }, "el antibiótico"),
        new Question("What is the Spanish word for 'aspirin'?", new List<string> { "la aspirina", "el antibiótico", "el medicamento", "la pastilla" }, "la aspirina"),
        new Question("What is the Spanish word for 'medication'?", new List<string> { "el medicamento", "la aspirina", "el antibiótico", "la pastilla" }, "el medicamento"),
        new Question("What is the Spanish word for 'pill'?", new List<string> { "la pastilla", "la aspirina", "el antibiótico", "el medicamento" }, "la pastilla"),
        new Question("What is the Spanish word for 'prescription'?", new List<string> { "la receta", "la aspirina", "el antibiótico", "el medicamento" }, "la receta"),

        // Vocabulary Questions from Set 2
        new Question("What is the Spanish word for 'clothes'?", new List<string> { "la ropa", "el abrigo", "los (blue) jeans", "la blusa" }, "la ropa"),
        new Question("What is the Spanish word for 'coat'?", new List<string> { "el abrigo", "la blusa", "la bolsa", "la bota" }, "el abrigo"),
        new Question("What is the Spanish word for 'jeans'?", new List<string> { "los (blue) jeans", "la blusa", "el abrigo", "la bolsa" }, "los (blue) jeans"),
        new Question("What is the Spanish word for 'blouse'?", new List<string> { "la blusa", "la bolsa", "el abrigo", "la bota" }, "la blusa"),
        new Question("What is the Spanish word for 'purse; bag'?", new List<string> { "la bolsa", "la blusa", "el abrigo", "la bota" }, "la bolsa"),
        new Question("What is the Spanish word for 'boot'?", new List<string> { "la bota", "la bolsa", "la blusa", "el abrigo" }, "la bota"),
        new Question("What is the Spanish word for 'sock(s)'?", new List<string> { "los calcetines (el calcetín)", "la bota", "la bolsa", "la blusa" }, "los calcetines (el calcetín)"),
        new Question("What is the Spanish word for 'shirt'?", new List<string> { "la camisa", "los (blue) jeans", "la bolsa", "el abrigo" }, "la camisa"),
        new Question("What is the Spanish word for 't-shirt'?", new List<string> { "la camiseta", "la blusa", "el abrigo", "la bota" }, "la camiseta"),
        new Question("What is the Spanish word for 'wallet'?", new List<string> { "la cartera", "la bolsa", "el abrigo", "la blusa" }, "la cartera"),
        new Question("What is the Spanish word for 'jacket'?", new List<string> { "la chaqueta", "la bota", "la bolsa", "la blusa" }, "la chaqueta"),
        new Question("What is the Spanish word for 'belt'?", new List<string> { "el cinturón", "la blusa", "el abrigo", "la bolsa" }, "el cinturón"),
        new Question("What is the Spanish word for 'tie'?", new List<string> { "la corbata", "la bolsa", "el abrigo", "la blusa" }, "la corbata"),
        new Question("What is the Spanish word for 'skirt'?", new List<string> { "la falda", "la blusa", "el abrigo", "la bolsa" }, "la falda"),
        new Question("What is the Spanish word for '(sun)glasses'?", new List<string> { "las gafas (de sol)", "la blusa", "el abrigo", "la bolsa" }, "las gafas (de sol)"),
        new Question("What is the Spanish word for 'gloves'?", new List<string> { "los guantes", "la bolsa", "el abrigo", "la blusa" }, "los guantes"),
        new Question("What is the Spanish word for 'raincoat'?", new List<string> { "el impermeable", "la blusa", "el abrigo", "la bolsa" }, "el impermeable"),
        new Question("What is the Spanish word for 'pants'?", new List<string> { "los pantalones", "la blusa", "el abrigo", "la bolsa" }, "los pantalones"),
        new Question("What is the Spanish word for 'shorts'?", new List<string> { "los pantalones cortos", "la blusa", "el abrigo", "la bolsa" }, "los pantalones cortos"),
        new Question("What is the Spanish word for 'clothing size'?", new List<string> { "la talla", "el abrigo", "la blusa", "la bolsa" }, "la talla"),
        new Question("What is the Spanish word for 'swimsuit'?", new List<string> { "el traje de baño", "la blusa", "el abrigo", "la bolsa" }, "el traje de baño"),
        new Question("What is the Spanish word for 'dress'?", new List<string> { "el vestido", "la blusa", "el abrigo", "la bolsa" }, "el vestido"),
        new Question("What is the Spanish word for 'salesperson'?", new List<string> { "el / la dependiente/a", "la blusa", "el abrigo", "la bolsa" }, "el / la dependiente/a"),
        new Question("What is the Spanish word for 'client'?", new List<string> { "el / la cliente/a", "la blusa", "el abrigo", "la bolsa" }, "el / la cliente/a"),
        new Question("What is the Spanish word for 'cash'?", new List<string> { "el dinero en efectivo", "la blusa", "el abrigo", "la bolsa" }, "el dinero en efectivo"),
        new Question("What is the Spanish word for 'store'?", new List<string> { "la tienda", "la bolsa", "el abrigo", "la blusa" }, "la tienda"),
        new Question("What is the Spanish word for 'sale'?", new List<string> { "la rebaja", "el abrigo", "la blusa", "la bolsa" }, "la rebaja"),
        new Question("What is the Spanish word for 'shopping mall'?", new List<string> { "el centro comercial", "la blusa", "el abrigo", "la bolsa" }, "el centro comercial"),
        new Question("What is the Spanish word for 'shop assistant'?", new List<string> { "el vendedor", "el cliente", "el dependiente", "la dependienta" }, "el vendedor"),
        new Question("What is the Spanish word for 'to sell'?", new List<string> { "vender", "comprar", "llevar", "gastar" }, "vender"),
        new Question("What is the Spanish word for 'to spend (money)'?", new List<string> { "gastar", "llevar", "comprar", "vender" }, "gastar"),
        new Question("What is the Spanish word for 'to wear; to take'?", new List<string> { "llevar", "comprar", "gastar", "vender" }, "llevar"),
        new Question("What is the Spanish word for 'to buy'?", new List<string> { "comprar", "llevar", "gastar", "vender" }, "comprar"),

        // Grammar Questions
        new Question("Which is the correct preterite form of 'tener' (to have)?", new List<string> { "tuve", "tenía", "tengo", "tenido" }, "tuve"),
        new Question("Which is the correct preterite form of 'ser' (to be)?", new List<string> { "fui", "era", "soy", "sido" }, "fui"),
        new Question("Which is the correct preterite form of 'ir' (to go)?", new List<string> { "fui", "iba", "voy", "ido" }, "fui"),
        new Question("Which is the correct imperfect form of 'tener' (to have)?", new List<string> { "tenía", "tuve", "tengo", "tenido" }, "tenía"),
        new Question("Which is the correct imperfect form of 'ser' (to be)?", new List<string> { "era", "fui", "soy", "sido" }, "era"),
        new Question("Which is the correct imperfect form of 'ir' (to go)?", new List<string> { "iba", "fui", "voy", "ido" }, "iba"),

        // Preterite vs. Imperfect Questions
        new Question("Which sentence uses the preterite correctly?", new List<string> { "Ayer comí pizza.", "Ayer comía pizza.", "Ayer comer pizza.", "Ayer comía la pizza." }, "Ayer comí pizza."),
        new Question("Which sentence uses the imperfect correctly?", new List<string> { "Siempre jugaba con mis amigos.", "Siempre jugué con mis amigos.", "Siempre jugamos con mis amigos.", "Siempre jugar con mis amigos." }, "Siempre jugaba con mis amigos."),
        new Question("Which sentence uses the preterite correctly?", new List<string> { "El año pasado viajé a España.", "El año pasado viajaba a España.", "El año pasado viajar a España.", "El año pasado viajó a España." }, "El año pasado viajé a España."),
        new Question("Which sentence uses the imperfect correctly?", new List<string> { "Cuando era niño, iba a la playa.", "Cuando era niño, fui a la playa.", "Cuando era niño, ir a la playa.", "Cuando era niño, iba la playa." }, "Cuando era niño, iba a la playa."),

        // Por vs. Para Questions
        new Question("Choose the correct usage of 'por' or 'para': This gift is for you.", new List<string> { "Este regalo es para ti.", "Este regalo es por ti.", "Este regalo es por tú.", "Este regalo es para tú." }, "Este regalo es para ti."),
        new Question("Choose the correct usage of 'por' or 'para': We walked through the park.", new List<string> { "Caminamos por el parque.", "Caminamos para el parque.", "Caminamos por la parque.", "Caminamos para la parque." }, "Caminamos por el parque."),
        new Question("Choose the correct usage of 'por' or 'para': The homework is due by Monday.", new List<string> { "La tarea es para el lunes.", "La tarea es por el lunes.", "La tarea es por lunes.", "La tarea es para lunes." }, "La tarea es para el lunes."),
        new Question("Choose the correct usage of 'por' or 'para': I studied for three hours.", new List<string> { "Estudié por tres horas.", "Estudié para tres horas.", "Estudié por horas tres.", "Estudié para horas tres." }, "Estudié por tres horas."),

        // Technology Questions
        new Question("What is the Spanish word for 'computer'?", new List<string> { "la computadora", "el ordenador", "la pantalla", "el teclado" }, "la computadora"),
        new Question("What is the Spanish word for 'keyboard'?", new List<string> { "el teclado", "la pantalla", "el ratón", "la computadora" }, "el teclado"),
        new Question("What is the Spanish word for 'mouse'?", new List<string> { "el ratón", "el teclado", "la pantalla", "la computadora" }, "el ratón"),
        new Question("What is the Spanish word for 'screen'?", new List<string> { "la pantalla", "la computadora", "el teclado", "el ratón" }, "la pantalla"),
        new Question("What is the Spanish word for 'internet'?", new List<string> { "el internet", "la red", "la web", "el sitio" }, "el internet"),
        new Question("What is the Spanish word for 'website'?", new List<string> { "el sitio web", "la red", "el internet", "la pantalla" }, "el sitio web"),
        new Question("What is the Spanish word for 'to download'?", new List<string> { "descargar", "subir", "instalar", "conectar" }, "descargar"),
        new Question("What is the Spanish word for 'to upload'?", new List<string> { "subir", "descargar", "instalar", "conectar" }, "subir"),
        new Question("What is the Spanish word for 'to install'?", new List<string> { "instalar", "subir", "descargar", "conectar" }, "instalar"),
        new Question("What is the Spanish word for 'to connect'?", new List<string> { "conectar", "descargar", "subir", "instalar" }, "conectar"),

        // Fun Facts about Colombia
        new Question("What is the capital of Colombia?", new List<string> { "Bogotá", "Medellín", "Cartagena", "Cali" }, "Bogotá"),
         new Question("What is the currency of Colombia?", new List<string> { "Peso colombiano", "Dólar estadounidense", "Euro", "Libra esterlina" }, "Peso colombiano"),
        new Question("Which Colombian artist is known as the 'Queen of Tejano Music'?", new List<string> { "Shakira", "Celia Cruz", "Selena Quintanilla", "Gloria Estefan" }, "Selena Quintanilla"),
        new Question("What is the official language of Colombia?", new List<string> { "Spanish", "Portuguese", "English", "French" }, "Spanish"),
        new Question("What is a popular Colombian dance style characterized by its energetic footwork?", new List<string> { "Salsa", "Tango", "Merengue", "Cumbia" }, "Cumbia"),
        new Question("Which Colombian city is famous for its annual Flower Festival?", new List<string> { "Medellín", "Bogotá", "Cali", "Cartagena" }, "Medellín"),

        // Fun Facts about Costa Rica
        new Question("What is the capital of Costa Rica?", new List<string> { "San José", "Lima", "Havana", "Panama City" }, "San José"),
        new Question("What is the currency of Costa Rica?", new List<string> { "Colón", "Dólar estadounidense", "Peso", "Euro" }, "Colón"),
        new Question("Which Costa Rican singer-songwriter released the hit song 'La Bicicleta' with Shakira?", new List<string> { "Carlos Vives", "Ricky Martin", "Juanes", "Maluma" }, "Carlos Vives"),
        new Question("What is the official language of Costa Rica?", new List<string> { "Spanish", "English", "Portuguese", "French" }, "Spanish"),
        new Question("Which animal is a symbol of Costa Rica and is featured on its national emblem?", new List<string> { "Quetzal", "Jaguar", "Sloth", "Toucan" }, "Sloth"),
        new Question("Costa Rica is known for its rich biodiversity. What percentage of the world's biodiversity is estimated to be found in Costa Rica?", new List<string> { "5%", "10%", "25%", "50%" }, "5%"),

        // Fun Facts about El Salvador
        new Question("What is the capital of El Salvador?", new List<string> { "San Salvador", "Guatemala City", "Managua", "Tegucigalpa" }, "San Salvador"),
        new Question("What is the currency of El Salvador?", new List<string> { "Dólar estadounidense", "Colón", "Peso", "Euro" }, "Dólar estadounidense"),
        new Question("Which famous Salvadoran archbishop was awarded the Nobel Peace Prize in 1980?", new List<string> { "Oscar Romero", "Augusto César Sandino", "Miguel Hidalgo", "Simon Bolivar" }, "Oscar Romero"),
        new Question("What is the official language of El Salvador?", new List<string> { "Spanish", "English", "Portuguese", "French" }, "Spanish"),
        new Question("What is the traditional Salvadoran dish made of thick corn tortillas filled with cheese, chicharrón, or refried beans?", new List<string> { "Pupusa", "Tamale", "Empanada", "Ceviche" }, "Pupusa"),
        new Question("Which active volcano, known as the 'Ilamatepec', is the highest volcano in El Salvador?", new List<string> { "Santa Ana Volcano", "Izalco Volcano", "San Miguel Volcano", "Chaparrastique Volcano" }, "Santa Ana Volcano"),

        // Fun Facts about Peru
        new Question("What is the capital of Peru?", new List<string> { "Lima", "Bogotá", "Quito", "Santiago" }, "Lima"),
        new Question("What is the currency of Peru?", new List<string> { "Sol", "Peso", "Dólar estadounidense", "Euro" }, "Sol"),
        new Question("Which ancient city, known as the 'Lost City of the Incas', is located in Peru?", new List<string> { "Machu Picchu", "Chichen Itza", "Tikal", "Nazca Lines" }, "Machu Picchu"),
        new Question("What is the official language of Peru?", new List<string> { "Spanish", "Quechua", "Aymara", "Portuguese" }, "Spanish"),
        new Question("Which Peruvian dish is made of raw fish cured in citrus juices and spiced with chili peppers?", new List<string> { "Ceviche", "Lomo Saltado", "Aji de Gallina", "Pollo a la Brasa" }, "Ceviche"),
        new Question("What is the name of the desert located in southern Peru known for its Nazca Lines geoglyphs?", new List<string> { "Atacama Desert", "Gobi Desert", "Sahara Desert", "Nazca Desert" }, "Atacama"),
        new Question("Which famous Peruvian singer gained international acclaim with songs like 'La Flor de la Canela' and 'El Cóndor Pasa'?", new List<string> { "Yma Sumac", "Tania Libertad", "Susana Baca", "Chabuca Granda" }, "Chabuca Granda"),
        new Question("What is the traditional Peruvian alcoholic beverage made from distilled grapes?", new List<string> { "Pisco", "Chicha", "Inca Kola", "Aguardiente" }, "Pisco"),
        // Por vs. Para Questions
new Question("Me compraron un regalo _____ mi cumpleaños.", new List<string> { "Para", "Por" }, "Por"),
new Question("Estudio mucho _____ obtener buenas notas.", new List<string> { "Para", "Por" }, "Para"),
new Question("Gracias _____ tu ayuda.", new List<string> { "Para", "Por" }, "Por"),
new Question("Voy a la tienda _____ comprar pan.", new List<string> { "Para", "Por" }, "Para"),
new Question("Hicieron una donación _____ una buena causa.", new List<string> { "Para", "Por" }, "Por"),
new Question("Este regalo es _____ mi mamá.", new List<string> { "Para", "Por" }, "Para"),
new Question("¿_____ qué quieres estudiar medicina?", new List<string> { "Para", "Por" }, "Por"),
new Question("Salimos temprano _____ evitar el tráfico.", new List<string> { "Para", "Por" }, "Para"),
new Question("Estos zapatos son _____ correr.", new List<string> { "Para", "Por" }, "Para"),
new Question("_____ favor, cierra la puerta.", new List<string> { "Para", "Por" }, "Por"),
new Question("Este libro es _____ aprender sobre historia.", new List<string> { "Para", "Por" }, "Para"),
new Question("Gracias _____ el regalo, me encanta.", new List<string> { "Para", "Por" }, "Por"),
new Question("El tren sale _____ Madrid.", new List<string> { "Para", "Por" }, "Para"),
new Question("_____ mí, estudiar es una prioridad.", new List<string> { "Para", "Por" }, "Para"),
new Question("Tienes que terminar el proyecto _____ el viernes.", new List<string> { "Para", "Por" }, "Para"),
new Question("Este regalo es _____ ti.", new List<string> { "Para", "Por" }, "Para"),
new Question("Viajamos _____ avión.", new List<string> { "Para", "Por" }, "Por"),
new Question("Lo hice _____ ti.", new List<string> { "Para", "Por" }, "Por"),
new Question("Compré el libro _____ aprender más.", new List<string> { "Para", "Por" }, "Para"),
new Question("¿_____ qué me preguntas eso?", new List<string> { "Para", "Por" }, "Por"),
new Question("La carta está _____ enviar.", new List<string> { "Para", "Por" }, "Para"),
new Question("Este regalo es _____ mi hermana.", new List<string> { "Para", "Por" }, "Para"),
new Question("Pagaron mucho dinero _____ ese cuadro.", new List<string> { "Para", "Por" }, "Por"),
new Question("Estoy aquí _____ ayudarte.", new List<string> { "Para", "Por" }, "Para"),
new Question("Este regalo es _____ mi padre.", new List<string> { "Para", "Por" }, "Para"),
new Question("Te lo doy _____ tu cumpleaños.", new List<string> { "Para", "Por" }, "Por"),
new Question("Fue al supermercado _____ comprar comida.", new List<string> { "Para", "Por" }, "Por"),
new Question("Vamos al cine _____ ver esa película.", new List<string> { "Para", "Por" }, "Para"),
new Question("Lo hice _____ mejorar la situación.", new List<string> { "Para", "Por" }, "Por"),
new Question("_____, estoy contento con tu decisión.", new List<string> { "Para", "Por" }, "Por"),
new Question("Por favor, voten _____ mi candidato.", new List<string> { "Para", "Por" }, "Por"),
new Question("Viajaron _____ Europa.", new List<string> { "Para", "Por" }, "Por"),
new Question("Este pastel es _____ tu cumpleaños.", new List<string> { "Para", "Por" }, "Para"),
new Question("¿_____ dónde quieres que nos encontremos?", new List<string> { "Para", "Por" }, "Por"),
new Question("Fue al gimnasio _____ hacer ejercicio.", new List<string> { "Para", "Por" }, "Por"),
new Question("_____, la situación está fuera de control.", new List<string> { "Para", "Por" }, "Por"),
new Question("Aprobé el examen _____ estudiar mucho.", new List<string> { "Para", "Por" }, "Por"),
new Question("Hablamos mucho tiempo _____ teléfono.", new List<string> { "Para", "Por" }, "Por"),
new Question("Me duele la cabeza _____ estudiar tanto.", new List<string> { "Para", "Por" }, "Por"),
new Question("Este regalo es _____ el Día de la Madre.", new List<string> { "Para", "Por" }, "Para"),
new Question("Pagaron mucho dinero _____ ese coche.", new List<string> { "Para", "Por" }, "Por"),
new Question("Estamos aquí _____ ayudarte.", new List<string> { "Para", "Por" }, "Para"),
new Question("Te felicito _____ tu éxito.", new List<string> { "Para", "Por" }, "Por"),
new Question("Este regalo es _____ el bebé.", new List<string> { "Para", "Por" }, "Para"),
new Question("Voy al supermercado _____ comprar leche.", new List<string> { "Para", "Por" }, "Para"),
new Question("Me duele la espalda _____ dormir mal.", new List<string> { "Para", "Por" }, "Por"),
new Question("Trabajo _____ ganar dinero.", new List<string> { "Para", "Por" }, "Para"),

// Preterite vs. Imperfect Questions
new Question("Ayer, ellos (hablar) ______ sobre sus vacaciones.", new List<string> { "preterite", "imperfect" }, "preterite"),
new Question("Cuando era niño, siempre (jugar) ______ con mis juguetes favoritos.", new List<string> { "preterite", "imperfect" }, "imperfect"),
new Question("Ayer, nosotros (visitar) ______ el museo de arte moderno.", new List<string> { "preterite", "imperfect" }, "preterite"),
new Question("Mientras ellos (caminar) ______ por el parque, comenzó a llover.", new List<string> { "preterite", "imperfect" }, "imperfect"),
new Question("Cuando (llegar) ______, ya se habían ido.", new List<string> { "preterite", "imperfect" }, "llegué"),
new Question("Todos los días (comer) ______ en ese restaurante.", new List<string> { "preterite", "imperfect" }, "imperfect")

    };



            return questions;
        }

        static void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        static void EndSession(int score, int totalQuestions, List<string> sessionAnswers)
        {
            double percentage = (double)score / totalQuestions * 100;
            Console.WriteLine($"Session ended. You scored {score} out of {totalQuestions} ({percentage:F2}%).");

            // Save session data to a file
            SaveSessionData(score, totalQuestions, sessionAnswers);

            // Calculate average percentage of correct answers
            double averagePercentage = CalculateAveragePercentage();
            Console.WriteLine($"Average percentage of correct answers: {averagePercentage:F2}%");
        }

        static void SaveSessionData(int score, int totalQuestions, List<string> sessionAnswers)
        {
            string filePath = "session_data.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true)) // Append mode
            {
                writer.WriteLine($"Score: {score}/{totalQuestions} ({(double)score / totalQuestions * 100:F2}%)");
                writer.WriteLine("Session Answers:");
                for (int i = 0; i < sessionAnswers.Count; i++)
                {
                    writer.WriteLine($"{i + 1}. {sessionAnswers[i]}");
                }
            }

            Console.WriteLine($"Session data saved to: {filePath}");
        }

        static double CalculateAveragePercentage()
        {
            string filePath = "session_data.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No session data found.");
                return 0;
            }

            List<double> percentages = new List<double>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Score:"))
                    {
                        string[] parts = line.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        string percentagePart = parts[1].Replace("%", "");
                        double percentage = double.Parse(percentagePart);
                        percentages.Add(percentage);
                    }
                }
            }

            if (percentages.Count == 0)
            {
                Console.WriteLine("No session data found.");
                return 0;
            }

            double averagePercentage = percentages.Average();
            return averagePercentage;
        }
    }

    class Question
    {
        public string Prompt { get; }
        public List<string> Options { get; }
        public string Answer { get; }

        public Question(string prompt, List<string> options, string answer)
        {
            Prompt = prompt;
            Options = options;
            Answer = answer;
        }
    }
}
