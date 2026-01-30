
// Get user input for name and marks
const readline = require('readline');

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

// Function to validate marks
function validateMarks(mark) {
    return mark >= 0 && mark <= 100;
}

// Function to determine pass/fail
function determinePassFail(marks) {
    return marks.map(mark => mark >= 40 ? 'PASS' : 'FAIL');
}

// Function to display results using string methods
function displayResults(studentName, subjects, marks, results) {
    console.log('\nSTUDENT RESULTS');
    console.log(`Student Name: ${studentName.toUpperCase()}`);
   // console.log('==========================================\n');

    subjects.forEach((subject, index) => {
        const result = `${subject.padEnd(20)} | Marks: ${marks[index].toString().padStart(3)} | Status: ${results[index].padStart(4)}`;
        console.log(result);
    });

    const totalMarks = marks.reduce((sum, mark) => sum + mark, 0);
    const averageMarks = (totalMarks / marks.length).toFixed(2);
    const passCount = results.filter(status => status === 'PASS').length;
    const failCount = results.filter(status => status === 'FAIL').length;

    console.log('\n==========================================');
    console.log(`Total Marks: ${totalMarks}/${marks.length * 100}`);
    console.log(`Average Marks: ${averageMarks}`);
    console.log(`Passed: ${passCount} | Failed: ${failCount}`);
    console.log('==========================================\n');

    // Overall Result
    const overallStatus = passCount === marks.length ? 'PASSED ALL SUBJECTS' : 'FAILED SOME SUBJECTS';
    console.log(`Overall Status: ${overallStatus.toLowerCase().replace(/\b\w/g, char => char.toUpperCase())}`);
    console.log('========================================\n');
}

// Main execution
console.log('STUDENT RESULT MANAGEMENT SYSTEM \n');

rl.question('Enter Student Name: ', (studentName) => {
    if (!studentName.trim()) {
        console.log('Invalid name. Exiting...');
        rl.close();
        return;
    }

    // Array of subjects
    const subjects = ['Mathematics', 'English', 'Science', 'History', 'Computer Science'];
    const marks = [];

    console.log(`\nEnter marks for ${studentName} (out of 100):\n`);

    let subjectIndex = 0;

    // Function to ask for each subject's marks
    const askForMarks = () => {
        if (subjectIndex >= subjects.length) {
            // All marks entered, calculate results
            const results = determinePassFail(marks);
            displayResults(studentName, subjects, marks, results);
            rl.close();
            return;
        }

        rl.question(`${subjects[subjectIndex]}: `, (input) => {
            const mark = parseFloat(input);

            if (!validateMarks(mark) || isNaN(mark)) {
                console.log('⚠️  Invalid marks! Please enter a number between 0 and 100.');
                askForMarks();
            } else {
                marks.push(mark);
                subjectIndex++;
                askForMarks();
            }
        });
    };

    askForMarks();
});
