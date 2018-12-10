function ausgangssignal = faltung(eingangssignal, impulsantwort)

ausgangssignal = zeros(1,size(impulsantwort,2)+1);

for t = 1:size(ausgangssignal,2)
   for u = 1:size(impulsantwort,2)
       if((t-u) >= 1 && (t-u)<= size(eingangssignal,2) && t<= size(ausgangssignal,2) && u <= size(impulsantwort,2))
       ausgangssignal(t) = ausgangssignal(t)+(eingangssignal(t-u) * impulsantwort(u));
       else
           
       end
   end
end
end